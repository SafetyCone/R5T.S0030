using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar.Results;

using R5T.F0001.F002;
using R5T.T0064;
using R5T.T0065;

using R5T.S0030;
using R5T.S0030.Library;

using IServiceImplementation = R5T.T0064.IServiceImplementation;

using Glossary = R5T.S0030.Y000.Glossary;

using Instances = R5T.S0030.Instances;


namespace System
{
    using N8;


    public static partial class IServiceImplementationOperatorExtensions
    {
        public static FunctionResult<string> HasImplementsServiceDefinitionAttribute(this IServiceImplementationOperator _,
            ClassDeclarationSyntax classDeclaration,
            CompilationUnitSyntax compilationUnit,
            IList<string> availableDefinitionNamespacedTypeNames)
        {
            var hasAttribute = classDeclaration.HasAttributeOfType<ImplementsServiceDefinitionAttribute>();
            if (hasAttribute)
            {
                var attribute = classDeclaration.GetAttributeOfTypeSingle<ImplementsServiceDefinitionAttribute>();

                var firstArgument = attribute.ArgumentList.Arguments.First();

                if (firstArgument.Expression is TypeOfExpressionSyntax typeOfExpressionSyntax)
                {
                    // Guess the type provided for the typeof operator.
                    compilationUnit = compilationUnit.AnnotateNode_Typed(
                        typeOfExpressionSyntax.Type,
                        out var typedAnnotation);

                    var attributeDefinitionNamespacedTypeNameWasFound = Instances.Operation.TryGuessNamespacedTypeName(
                        availableDefinitionNamespacedTypeNames,
                        compilationUnit,
                        typedAnnotation);

                    if (attributeDefinitionNamespacedTypeNameWasFound)
                    {
                        return FunctionResult.Success(attributeDefinitionNamespacedTypeNameWasFound.Result);
                    }
                    else
                    {
                        return FunctionResult.Failure<string>($"Implementation had {nameof(ImplementsServiceDefinitionAttribute)} attribute, but it's type was not recognized as a service definition.");
                    }
                }
                else
                {
                    return FunctionResult.Failure<string>($"Implementation had {nameof(ImplementsServiceDefinitionAttribute)} attribute, but its first argument type parameter could not be handled (was not a nameof() expression).");
                }
            }
            else
            {
                return FunctionResult.Failure<string>($"Implementation did not have {nameof(ImplementsServiceDefinitionAttribute)} attribute.");
            }
        }

        public static ActionResult HasServiceImplementationMarkerAttribute(this IServiceImplementationOperator _,
            ClassDeclarationSyntax classDeclaration)
        {
            var hasMarkerAttribute = Instances.ClassOperator_Old.HasServiceImplementationMarkerAttribute(
                classDeclaration);

            var output = hasMarkerAttribute
                ? ActionResult.Success()
                : ActionResult.Failure($"Service implementation does not have the {nameof(ServiceImplementationMarkerAttribute)}.")
                ;

            return output;
        }

        public static ActionResult HasServiceImplementationMarkerInterface(this IServiceImplementationOperator _,
            ClassDeclarationSyntax classDeclaration,
            CompilationUnitSyntax compilationUnit)
        {
            var hasMarkerInterface = Instances.ClassOperator_Old.HasServiceImplementationMarkerInterface(
                classDeclaration,
                compilationUnit);

            var output = hasMarkerInterface
                ? ActionResult.Success()
                : ActionResult.Failure($"Service implementation does not have the {nameof(IServiceImplementation)} interface.")
                ;

            return output;
        }

        public static ActionResult HasValidConstructor(this IServiceImplementationOperator _,
            ClassDeclarationSyntax classDeclaration,
            CompilationUnitSyntax compilationUnit,
            IList<string> availableDefinitionNamespacedTypeNames)
        {
            var constructors = classDeclaration.GetConstructors().Now();

            var constructorCount = constructors.Length;
            if (constructorCount < 1)
            {
                return ActionResult.Success("Zero constructors found.");
            }

            // Use the first constructor regardless of whether there are one or more.
            // If there are more, and a constructor other than the first has the marker attribute, the first constructor with the marker attribute will be used.
            var constructor = constructors.First();

            if (constructorCount > 1)
            {
                // Do any of the constructors have the T0064.ServiceImplementationConstructorMarkerAttribute?
                var constructorsWithMarkerAttribute = constructors
                    .Where(x => x.HasAttributeOfType<ServiceImplementationConstructorMarkerAttribute>())
                    .Now();

                var constructorsWithMarkerAttributeCount = constructorsWithMarkerAttribute.Length;

                if (constructorsWithMarkerAttributeCount < 1)
                {
                    return ActionResult.Failure($"Multiple constructors found for service implementation, but none had the {nameof(ServiceImplementationConstructorMarkerAttribute)} attribute.");
                }
                else
                {
                    // The first constructor with the marker attribute will be used regardless of wehther ther are one or more.
                    constructor = constructorsWithMarkerAttribute.First();

                    if (constructorsWithMarkerAttributeCount > 1)
                    {
                        return ActionResult.Warning($"Multiple constructors found for service implementation with the {nameof(ServiceImplementationConstructorMarkerAttribute)} attribute. Thus, the first constructor with the attribute will be used.");
                    }
                }
            }

            var allConstructorParameters = constructor
                .GetParameters()
                .Now();

            // We only need to worry about parameters that are not explicitly marked as being service components.
            var unignoredParameters = allConstructorParameters
                // Allow implementation author to say "trust me, this is a service component, even if it is not register".
                .Where(xParameter => !xParameter.HasAttributeOfType<ParameterServiceComponentMarkerAttribute>())
                // Allow service implementations to take non-service component instances (useful for constructor-based provider implementations where the constructor takes a data value).
                .Where(xParameter => !xParameter.HasAttributeOfType<NotServiceComponentAttribute>())
                .Now();

            var parameterTypes = unignoredParameters
                .Select(Instances.Operation.GetParameterType_HandleIEnumerable)
                .Now();

            var parametersCount = parameterTypes.Length;

            if (parametersCount < 1)
            {
                return ActionResult.Success("No service dependencies found: no input parameters for constructor method found.");
            }

            compilationUnit = compilationUnit.AnnotateNodes_Typed(
                parameterTypes,
                out var typedAnnotationsByParameterTypes);

            var parameterTypesByTypedAnnotation = typedAnnotationsByParameterTypes.Invert();

            var parameterDefinitionNamespacedTypeNamesByTypedAnnotation = Instances.Operation.TryGuessNamespacedTypeNames(
                availableDefinitionNamespacedTypeNames,
                compilationUnit,
                parameterTypesByTypedAnnotation.Keys);

            var parameterDefinitionNamespacedTypeNamesFound = parameterDefinitionNamespacedTypeNamesByTypedAnnotation.Values
                .Where(x => x.Exists)
                .Select(x => x.Result)
                .Now();

            var allParametersAreDefinitions = parameterTypes.Length == parameterDefinitionNamespacedTypeNamesFound.Length;
            if (!allParametersAreDefinitions)
            {
                return ActionResult.Failure("Not all input parameters of the chosen constructor were service definitions found among the available service definitions for implementation class.");
            }

            return ActionResult.Success();
        }

        /// <summary>
        /// <inheritdoc cref="Glossary.ForServiceImplementation.Requirements.Level_00" path="/summary"/>
        /// </summary>
        public static MultipleActionResult SatisfiesRequirements_Level_00(this IServiceImplementationOperator _,
            TypeDeclarationSyntax typeDeclaration)
        {
            var isClass = _.IsClass(typeDeclaration);
            if(!isClass)
            {
                return MultipleActionResult.From(
                    ActionResult.Failure("Service implementation must be a class."));
            }

            return MultipleActionResult.Success();
        }

        /// <summary>
        /// <inheritdoc cref="Glossary.ForServiceImplementation.Requirements.Level_01" path="/summary"/>
        /// </summary>
        public static MultipleActionResult SatisfiesRequirements_Level_01(this IServiceImplementationOperator _,
            ClassDeclarationSyntax classDeclaration)
        {
            var isAbstract = !_.IsNotAbstract(classDeclaration);
            var isAbstractResult = isAbstract
                ? ActionResult.Failure("Service implementation must not be abstract.")
                : ActionResult.Success()
                ;

            var isStatic = !_.IsNotStatic(classDeclaration);
            var isStaticResult = isStatic
                ? ActionResult.Failure("Service implementation must not be static.")
                : ActionResult.Success()
                ;

            var output = MultipleActionResult.From_UnsuccessfulOnlyOrSingleSuccess(
                isAbstractResult,
                isStaticResult);

            return output;
        }

        /// <summary>
        /// <inheritdoc cref="Glossary.ForServiceImplementation.Requirements.Level_02" path="/summary"/>
        /// </summary>
        public static MultipleActionResult SatisfiesRequirements_Level_02(this IServiceImplementationOperator _,
            ClassDeclarationSyntax classDeclaration,
            CompilationUnitSyntax compilationUnit,
            IList<string> availableDefinitionNamespacedTypeNames)
        {
            /// Special cases.
            // Does the class speccify a service definition via the implements-service-definition attribute?
            var hasImplementsServiceDefinitionAttribute = _.HasImplementsServiceDefinitionAttribute(
                classDeclaration,
                compilationUnit,
                availableDefinitionNamespacedTypeNames);

            if(hasImplementsServiceDefinitionAttribute.Succeeded())
            {
                return MultipleActionResult.From(
                    hasImplementsServiceDefinitionAttribute.GetActionResult());
            }

            // Does the class have the no-service-definition interface?
            var hasNoServiceDefinitionBaseType = Instances.Operation.HasBaseType(
                classDeclaration,
                compilationUnit,
                Instances.NamespacedTypeName.R5T_T0064_INoServiceDefinition());

            if (hasNoServiceDefinitionBaseType.Succeeded())
            {
                return MultipleActionResult.From(
                    hasNoServiceDefinitionBaseType.GetActionResult());
            }
            // Else
            var noServiceDefinitionBaseTypeNotFound = ActionResult.Failure($"The type did not have the {nameof(R5T.T0064.INoServiceDefinition)} interface among its base types.");

            /// Main case.
            // Does the base class have one of the available service definitions among its base type list?
            var hasServiceDefinitionBaseType = Instances.Operation.HasBaseType(
                classDeclaration,
                compilationUnit,
                availableDefinitionNamespacedTypeNames);

            if(hasServiceDefinitionBaseType.Succeeded())
            {
                return MultipleActionResult.From(
                    hasServiceDefinitionBaseType.GetActionResult());
            }

            // Else, both failed.
            var output = MultipleActionResult.From(
                // Order is important for output to summary text file.
                hasServiceDefinitionBaseType.GetActionResult(),
                hasImplementsServiceDefinitionAttribute.GetActionResult(),
                noServiceDefinitionBaseTypeNotFound);

            return output;
        }

        /// <summary>
        /// <inheritdoc cref="Glossary.ForServiceImplementation.Requirements.Level_03" path="/summary"/>
        /// </summary>
        public static MultipleActionResult SatisfiesRequirements_Level_03(this IServiceImplementationOperator _,
            ClassDeclarationSyntax classDeclaration,
            CompilationUnitSyntax compilationUnit,
            IList<string> availableDefinitionNamespacedTypeNames)
        {
            var hasValidConstructor = _.HasValidConstructor(
                classDeclaration,
                compilationUnit,
                availableDefinitionNamespacedTypeNames);

            var output = MultipleActionResult.From(hasValidConstructor);
            return output;
        }

        /// <summary>
        /// <inheritdoc cref="Glossary.ForServiceImplementation.Requirements.Level_04" path="/summary"/>
        /// </summary>
        public static MultipleActionResult SatisfiesRequirements_Level_04(this IServiceImplementationOperator _,
            ClassDeclarationSyntax classDeclarationSyntax,
            CompilationUnitSyntax compilationUnit)
        {
            var hasMarkerAttribute = _.HasServiceImplementationMarkerAttribute(classDeclarationSyntax);
            var hasMarkerInterface = _.HasServiceImplementationMarkerInterface(
                classDeclarationSyntax,
                compilationUnit);

            var output = MultipleActionResult.From_UnsuccessfulOnlyOrSingleSuccess(
                hasMarkerAttribute,
                hasMarkerInterface);

            return output;
        }

        /// <summary>
        /// <inheritdoc cref="Glossary.ForServiceImplementation.Requirements.Level_04" path="/summary"/>
        /// </summary>
        public static MultipleActionResult SatisfiesRequirements_Level_04(this IServiceImplementationOperator _,
            ClassDeclarationSyntax classDeclarationSyntax)
        {
            var compilationUnit = classDeclarationSyntax.GetContainingCompilationUnit();

            var output = _.SatisfiesRequirements_Level_04(
                classDeclarationSyntax,
                compilationUnit);

            return output;
        }

        /// <summary>
        /// <inheritdoc cref="Glossary.ForServiceImplementation.Requirements.Level_00" path="/summary"/>
        /// </summary>
        public static MultipleActionResult SatisfiesRequirementsUpTo_Level_00(this IServiceImplementationOperator _,
            TypeDeclarationSyntax typeDeclaration)
        {
            var satisfiesLevel00 = _.SatisfiesRequirements_Level_00(typeDeclaration);

            var output = MultipleActionResult.From(satisfiesLevel00);
            return output;
        }

        /// <summary>
        /// <inheritdoc cref="Glossary.ForServiceImplementation.Requirements.Level_01" path="/summary"/>
        /// <inheritdoc cref="Glossary.ForServiceImplementation.Requirements.Level_00" path="/summary"/>
        /// </summary>
        public static MultipleActionResult SatisfiesRequirementsUpTo_Level_01(this IServiceImplementationOperator _,
            ClassDeclarationSyntax classDeclaration)
        {
            // Due to the input type we know the class declaration is a class. So because the Level 00 requires are (so far) only that the type is a class, we might think to skip checking those level 00 requirements.
            // However, level 00 requirements might change in the future and we are working the the "requirements level" level of abstraction.
            // So, check them anyway.
            var satisfiesUpToLevel00 = _.SatisfiesRequirementsUpTo_Level_00(classDeclaration);

            var satisfiesLevel01 = _.SatisfiesRequirements_Level_01(classDeclaration);

            var output = MultipleActionResult.From_UnsuccessfulOnlyOrSingleSuccess(
                satisfiesUpToLevel00,
                satisfiesLevel01);

            return output;
        }

        /// <summary>
        /// <inheritdoc cref="Glossary.ForServiceImplementation.Requirements.Level_02" path="/summary"/>
        /// <inheritdoc cref="Glossary.ForServiceImplementation.Requirements.Level_01" path="/summary"/>
        /// <inheritdoc cref="Glossary.ForServiceImplementation.Requirements.Level_00" path="/summary"/>
        /// </summary>
        public static MultipleActionResult SatisfiesRequirementsUpTo_Level_02(this IServiceImplementationOperator _,
            ClassDeclarationSyntax classDeclaration,
            CompilationUnitSyntax compilationUnit,
            IList<string> availableDefinitionNamespacedTypeNames)
        {
            var satisfiesUpToLevel01 = _.SatisfiesRequirementsUpTo_Level_01(classDeclaration);

            var satisfiesLevel02 = _.SatisfiesRequirements_Level_02(
                classDeclaration,
                compilationUnit,
                availableDefinitionNamespacedTypeNames);

            var output = MultipleActionResult.From_UnsuccessfulOnlyOrSingleSuccess(
                satisfiesUpToLevel01,
                satisfiesLevel02);

            return output;
        }

        /// <summary>
        /// <inheritdoc cref="Glossary.ForServiceImplementation.Requirements.Level_03" path="/summary"/>
        /// <inheritdoc cref="Glossary.ForServiceImplementation.Requirements.Level_02" path="/summary"/>
        /// <inheritdoc cref="Glossary.ForServiceImplementation.Requirements.Level_01" path="/summary"/>
        /// <inheritdoc cref="Glossary.ForServiceImplementation.Requirements.Level_00" path="/summary"/>
        /// </summary>
        public static MultipleActionResult SatisfiesRequirementsUpTo_Level_03(this IServiceImplementationOperator _,
            ClassDeclarationSyntax classDeclaration,
            CompilationUnitSyntax compilationUnit,
            IList<string> availableDefinitionNamespacedTypeNames)
        {
            var satisfiesUpToLevel02 = _.SatisfiesRequirementsUpTo_Level_02(
                classDeclaration,
                compilationUnit,
                availableDefinitionNamespacedTypeNames);

            var satisfiesLevel03 = _.SatisfiesRequirements_Level_03(
                classDeclaration,
                compilationUnit,
                availableDefinitionNamespacedTypeNames);

            var output = MultipleActionResult.From_UnsuccessfulOnlyOrSingleSuccess(
                satisfiesUpToLevel02,
                satisfiesLevel03);

            return output;
        }

        /// <summary>
        /// <inheritdoc cref="Glossary.ForServiceImplementation.Requirements.Level_04" path="/summary"/>
        /// <inheritdoc cref="Glossary.ForServiceImplementation.Requirements.Level_03" path="/summary"/>
        /// <inheritdoc cref="Glossary.ForServiceImplementation.Requirements.Level_02" path="/summary"/>
        /// <inheritdoc cref="Glossary.ForServiceImplementation.Requirements.Level_01" path="/summary"/>
        /// <inheritdoc cref="Glossary.ForServiceImplementation.Requirements.Level_00" path="/summary"/>
        /// </summary>
        public static MultipleActionResult SatisfiesRequirementsUpTo_Level_04(this IServiceImplementationOperator _,
            ClassDeclarationSyntax classDeclaration,
            CompilationUnitSyntax compilationUnit,
            IList<string> availableDefinitionNamespacedTypeNames)
        {
            var satisfiesUpToLevel03 = _.SatisfiesRequirementsUpTo_Level_03(
                classDeclaration,
                compilationUnit,
                availableDefinitionNamespacedTypeNames);

            var satisfiesLevel04 = _.SatisfiesRequirements_Level_04(classDeclaration);

            var output = MultipleActionResult.From_UnsuccessfulOnlyOrSingleSuccess(
                satisfiesUpToLevel03,
                satisfiesLevel04);

            return output;
        }

        /// <summary>
        /// <inheritdoc cref="SatisfiesRequirementsUpTo_Level_04(IServiceImplementationOperator, ClassDeclarationSyntax, CompilationUnitSyntax, IList{string})"/>
        /// </summary>
        public static MultipleActionResult SatisfiesRequirements(this IServiceImplementationOperator _,
            ClassDeclarationSyntax classDeclaration,
            CompilationUnitSyntax compilationUnit,
            IList<string> availableDefinitionNamespacedTypeNames)
        {
            var output = _.SatisfiesRequirementsUpTo_Level_04(
                classDeclaration,
                compilationUnit,
                availableDefinitionNamespacedTypeNames);

            return output;
        }
    }
}


namespace N8
{
    public static class IServiceImplementationOperatorExtensions
    {
        /// <summary>
        /// <inheritdoc cref="Glossary.ForServiceImplementation.Requirements.MustBeClass" path="/reason"/>
        /// </summary>
        public static bool IsClass(this IServiceImplementationOperator _,
            TypeDeclarationSyntax typeDeclaration)
        {
            var isClass = typeDeclaration.IsClass();
            return isClass;
        }

        /// <summary>
        /// <inheritdoc cref="Glossary.ForServiceImplementation.Requirements.MustNotBeAbstract" path="/reason"/>
        /// </summary>
        public static bool IsNotAbstract(this IServiceImplementationOperator _,
            ClassDeclarationSyntax classDeclaration)
        {
            var isAbstract = classDeclaration.IsAbstract();

            var output = !isAbstract;
            return output;
        }

        /// <summary>
        /// <inheritdoc cref="Glossary.ForServiceImplementation.Requirements.MustNotBeStatic" path="/reason"/>
        /// </summary>
        public static bool IsNotStatic(this IServiceImplementationOperator _,
            ClassDeclarationSyntax classDeclaration)
        {
            var isStatic = classDeclaration.IsStatic();

            var output = !isStatic;
            return output;
        }
    }
}