using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar;
using R5T.Magyar.Results;

using R5T.T0098;

using R5T.S0030.T001.Level01;


namespace R5T.S0030.Library
{
    public static partial class IOperationExtensions
    {
        public static string[] DescribeSelectionProcessesOnly(this IOperation _,
            UpgradeResult upgradeResult)
        {
            var definitionSelectionMethod = upgradeResult.DefinitionSelectionMethod.ToString_Description();
            var dependenciesSelectionMethod = upgradeResult.DependencySetSelectionMethod.ToString_Description();

            var output = new[]
            {
                $"=> {definitionSelectionMethod}",
                $"=> {dependenciesSelectionMethod}",
            };

            return output;
        }

        public static string[] Describe(this IOperation _,
            T001.Level02.ImplementationDescriptor implementation)
        {
            var defintionNamespacedTypeNameDescription = implementation.HasServiceDefinition
                ? Instances.NamespacedTypeNameOperator.GetDescribedNamespacedTypeName(
                    implementation.ServiceDefinitionNamespacedTypeName)
                : String.Empty
                ;

            var serviceDefinitionLine = implementation.HasServiceDefinition
                ? $": {defintionNamespacedTypeNameDescription}\n"
                : ": < No service definition. >\n"
                ;

            var serviceDependenciesLine = implementation.HasServiceDependencies
                ? $"Service Dependencies:"
                : "< No service dependencies. >\n"
                ;

            var serviceDependencyLines = implementation.HasServiceDependencies
                ? implementation.ServiceDependencyNamespacedTypeNames
                    .OrderAlphabetically()
                    .Select(x => $"\t{x}")
                    .Append(String.Empty)
                : Enumerable.Empty<string>();
                ;

            var output = new[]
            {
                serviceDefinitionLine,
                serviceDependenciesLine
            }
            .AppendRange2(serviceDependencyLines)
            .Now();

            return output;
        }

        public static ServiceDefinitionDependencyDescriptor GetEmptyServiceDefinitionDependencyDescriptor(this IOperation _)
        {
            var output = new ServiceDefinitionDependencyDescriptor
            {
                TypeNameFragment = String.Empty,
                IsRecognizedServiceDefinitionType = false,
                ServiceDefinitionNamespacedTypeName = String.Empty,
            };

            return output;
        }

        public static TypeSyntax GetParameterType_HandleIEnumerable(this IOperation _,
            ParameterSyntax parameter)
        {
            // IEnumerable<T> is special, handle it.
            if (parameter.Type is object && parameter.Type is GenericNameSyntax genericName)
            {
                if (genericName.Identifier.ValueText == Instances.TypeName.IEnumerable())
                {
                    return genericName.TypeArgumentList.Arguments.First();
                }
            }

            // Else, return the type.
            // Type should not be null, but apparently can be, I think probably because the parameter syntax is used both on the caller and callee sides, and the caller would not specify the type.
            return parameter.Type;
        }

        /// <summary>
        /// Failure occurs whenever there is a contradiction implied by the presence of <see cref="T0064.SpecifyServiceDefinitionAttribute"/> or <see cref="T0064.INoServiceDefinitionMarker"/>.
        /// Success occurs otherwise.
        /// </summary>
        public static MultipleActionResult IsSuccess(this IOperation _,
            ImplementationValidationResult validationResult)
        {
            var actionResults = new List<ActionResult>();

            if (!validationResult.IsClass)
            {
                actionResults.Add(
                    ActionResult.Failure("Service implementation must be a class."));
            }

            if (validationResult.IsAbstract)
            {
                actionResults.Add(
                    ActionResult.Failure("Service implementation cannot be abstract."));
            }

            if(validationResult.IsStatic)
            {
                actionResults.Add(
                    ActionResult.Failure("Service implementation cannot be static."));
            }

            if(validationResult.SpecifyServiceDefinitionTypeUnrecognized)
            {
                actionResults.Add(
                    ActionResult.Failure($"{nameof(T0064.SpecifyServiceDefinitionAttribute)} type value unrecognized as service definition."));
            }

            if(validationResult.SpecifiedServiceDefinitionNotFoundInBaseTypes)
            {
                actionResults.Add(
                    ActionResult.Failure($"{nameof(T0064.SpecifyServiceDefinitionAttribute)} type not found in base types."));
            }

            if(validationResult.NoServiceDefinitionBaseTypeRecognized)
            {
                actionResults.Add(
                    ActionResult.Failure("No service definition recognized among base types."));
            }

            if(validationResult.MultipleServiceDefinitionBaseTypesWithoutMarkerAttribute)
            {
                actionResults.Add(
                    ActionResult.Warning($"Multiple base type service definitions without {nameof(T0064.SpecifyServiceDefinitionAttribute)} specifying which one should be chosen."));
            }

            if(validationResult.HasExplicitNoServiceDefinitionMarkerButHasDefinitionBaseType)
            {
                actionResults.Add(
                    ActionResult.Failure($"Service implementation has both {nameof(T0064.INoServiceDefinitionMarker)} (or {nameof(T0064.INoServiceDefinition)}) and a service definition among its base types."));
            }

            if(validationResult.HasExplicitNoServiceDefinitionMarkerButHasSpecifyDefinitionAttribute)
            {
                actionResults.Add(
                    ActionResult.Failure($"Service implementation has both {nameof(T0064.INoServiceDefinitionMarker)} (or {nameof(T0064.INoServiceDefinition)}) and the {nameof(T0064.SpecifyServiceDefinitionAttribute)} (or {nameof(T0064.ImplementsServiceDefinitionAttribute)}) attribute."));
            }

            if(validationResult.MultipleConstructorsWithoutMarkerAttribute)
            {
                actionResults.Add(
                    ActionResult.Warning($"Multiple constructors were found without the {nameof(T0064.ServiceImplementationConstructorMarkerAttribute)} attribute."));
            }

            if (validationResult.MultipleConstructorsWithMarkerAttribute)
            {
                actionResults.Add(
                    ActionResult.Warning($"Multiple constructors were found with the {nameof(T0064.ServiceImplementationConstructorMarkerAttribute)} attribute."));
            }

            if(validationResult.HasOldImplementsServiceDefinitionAttribute)
            {
                actionResults.Add(
                    ActionResult.Warning($"Service implementation has old {nameof(T0064.ImplementsServiceDefinitionAttribute)} attribute."));
            }

            if(validationResult.HasOldNoServiceDefinitionInterface)
            {
                actionResults.Add(
                    ActionResult.Warning($"Service implementation has old {nameof(T0064.INoServiceDefinition)} interface."));
            }

            if(validationResult.HasOldServiceImplementationInterface)
            {
                actionResults.Add(
                    ActionResult.Warning($"Service implementation has old {nameof(T0064.IServiceImplementation)} interface."));
            }

            var constructorIsSuccess = _.IsSuccess(validationResult.ChosenConstructorResult);

            actionResults.AddRange(constructorIsSuccess.ActionResults);

            var output = MultipleActionResult.From(actionResults);
            return output;
        }

        public static MultipleActionResult IsSuccess(this IOperation _,
            ConstructorValidationResult validationResult)
        {
            var actionResults = new List<ActionResult>();

            foreach (var parameter in validationResult.ParameterResults)
            {
                var isSuccess = _.IsSuccess(parameter);

                actionResults.AddRange(isSuccess.ActionResults);
            }

            var output = MultipleActionResult.From(actionResults);
            return output;
        }

        public static MultipleActionResult IsSuccess(this IOperation _,
            ParameterValidationResult validationResult)
        {
            var actionResults = new List<ActionResult>();

            if(validationResult.NotRecognizedAndMissingExplicitNotServiceComponentAttribute)
            {
                actionResults.Add(
                    ActionResult.Failure($"Parameter '{validationResult.ParameterName}' type not recognized as a service definition type and lacked {nameof(T0064.NotServiceComponentAttribute)} attribute."));
            }

            var output = MultipleActionResult.From(actionResults);
            return output;
        }

        public static UpgradeResult Upgrade(this IOperation _,
            ImplementationDescriptor implementation,
            out T001.Level02.ImplementationDescriptor upgradedImplementationDescriptor)
        {
            var validationResult = Instances.ServiceImplementationOperator.Validate(implementation);

            var validationSuccess = _.IsSuccess(validationResult);

            var upgradeResult = new UpgradeResult
            {
                NamespacedTypeName = implementation.NamespacedTypeName,

                ValidationResult = validationResult,
                ValidationSuccess = validationSuccess,
            };

            if (!upgradeResult.ValidationSuccess.Succeeded())
            {
                upgradedImplementationDescriptor = null;

                return upgradeResult;
            }

            upgradedImplementationDescriptor = new T001.Level02.ImplementationDescriptor
            {
                NamespacedTypeName = implementation.NamespacedTypeName,
            };

            if(implementation.HasExplicitNoServiceDefinitionMarkerInterface)
            {
                upgradeResult.DefinitionSelectionMethod = DefinitionSelectionMethod.ExplicitNoServiceDefinitionMarkerAttribute;

                upgradedImplementationDescriptor.HasServiceDefinition = false;
            }
            else
            {
                upgradedImplementationDescriptor.HasServiceDefinition = true;

                if(implementation.HasSpecifyServiceDefinitionAttribute)
                {
                    upgradeResult.DefinitionSelectionMethod = DefinitionSelectionMethod.SpecifyServiceDefinitionAttribute;

                    upgradedImplementationDescriptor.ServiceDefinitionNamespacedTypeName = implementation.SpecifyServiceDefinitionType.ServiceDefinitionNamespacedTypeName;
                }
                else
                {
                    var serviceDefinitionBaseTypeDescriptors = implementation.BaseTypeDescriptors
                        .Where(x => x.IsRecognizedServiceDefinitionType)
                        .Now();

                    if(serviceDefinitionBaseTypeDescriptors.Length < 2)
                    {
                        upgradeResult.DefinitionSelectionMethod = DefinitionSelectionMethod.OnlyServiceDefinitionBaseType;

                        upgradedImplementationDescriptor.ServiceDefinitionNamespacedTypeName = serviceDefinitionBaseTypeDescriptors.Single().ServiceDefinitionNamespacedTypeName;
                    }
                    else
                    {
                        upgradeResult.DefinitionSelectionMethod = DefinitionSelectionMethod.FirstServiceDefinitionBaseType;

                        upgradedImplementationDescriptor.ServiceDefinitionNamespacedTypeName = serviceDefinitionBaseTypeDescriptors.First().ServiceDefinitionNamespacedTypeName;
                    }
                }
            }

            var chosenConstructor = _.ChooseConstructor(
                implementation,
                out var dependencySetSelectionMethod);

            upgradeResult.DependencySetSelectionMethod = dependencySetSelectionMethod;

            if(chosenConstructor.Exists)
            {
                upgradedImplementationDescriptor.ServiceDependencyNamespacedTypeNames = chosenConstructor.Result.ParameterDescriptors
                    .Where(x => x.IsRecognizedServiceDefinitionType)
                    .Select(x => x.ServiceDefinitionNamespacedTypeName)
                    .Now();
            }
            else
            {
                upgradedImplementationDescriptor.ServiceDependencyNamespacedTypeNames = Array.Empty<string>();
            }

            upgradedImplementationDescriptor.HasServiceDependencies = upgradedImplementationDescriptor.ServiceDependencyNamespacedTypeNames.Any();

            return upgradeResult;
        }

        public static WasFound<ConstructorDescriptor> ChooseConstructor(this IOperation _,
            ImplementationDescriptor implementation,
            out DependencySetSelectionMethod dependencySetSelectionMethod)
        {
            if (implementation.ConstructorDescriptors.Length < 1)
            {
                dependencySetSelectionMethod = DependencySetSelectionMethod.NoConstructor;

                return WasFound.NotFound<ConstructorDescriptor>();
            }
            else
            {
                // Choose the first for now, will be updated later.
                var chosenConstructor = implementation.ConstructorDescriptors.First();

                if (implementation.ConstructorDescriptors.Length < 2)
                {
                    dependencySetSelectionMethod = DependencySetSelectionMethod.OnlyConstructor;

                    // Keep the first constructor as the chosen constructor.
                }
                else
                {
                    var markedConstructors = implementation.ConstructorDescriptors
                        .Where(x => x.HasServiceImplementationConstructorAttribute)
                        .Now();

                    if (markedConstructors.Length < 1)
                    {
                        dependencySetSelectionMethod = DependencySetSelectionMethod.FirstConstructor;

                        // Keep the first constructor as the chosen constructor.
                    }
                    else
                    {
                        // Use the first marked constructor in either case.
                        chosenConstructor = markedConstructors.First();

                        if (markedConstructors.Length < 2)
                        {
                            dependencySetSelectionMethod = DependencySetSelectionMethod.ExplicitServiceImplementationConstructorMarkerAttribute;

                        }
                        else
                        {
                            dependencySetSelectionMethod = DependencySetSelectionMethod.FirstExplicitServiceImplementationConstructorMarkerAttribute;
                        }
                    }
                }

                return WasFound.Found(chosenConstructor);
            }
        }
    }
}
