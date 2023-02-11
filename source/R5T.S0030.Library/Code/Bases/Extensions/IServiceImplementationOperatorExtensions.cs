using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar;

using R5T.F0001.F002;
using R5T.T0065;

using R5T.S0030.T001.Level01;



namespace R5T.S0030.Library
{
    public static class IServiceImplementationOperatorExtensions
    {
        public static ImplementationDescriptor Describe(this IServiceImplementationOperator _,
            ClassDeclarationSyntax classDeclaration,
            CompilationUnitSyntax compilationUnit,
            IList<string> availableDefinitionNamespacedTypeNames)
        {
            // Create variables as aides during debugging.
            var namespacedTypeName = classDeclaration.GetNamespacedTypeName_HandlingTypeParameters();

            var isClass = classDeclaration.IsClass(); // Duh! But ok.
            var isAbstract = classDeclaration.IsAbstract();
            var isStatic = classDeclaration.IsStatic();

            var hasExplicitNoServiceDefinitionMarkerInterface = Instances.Operation.HasBaseTypeOfType<T0064.INoServiceDefinitionMarker>(
                classDeclaration,
                compilationUnit)
                .Exists;
            var hasOldNoServiceDefinitionInterface = Instances.Operation.HasBaseTypeOfType<T0064.INoServiceDefinition>(
                classDeclaration,
                compilationUnit)
                .Exists;
            if(!hasExplicitNoServiceDefinitionMarkerInterface && hasOldNoServiceDefinitionInterface)
            {
                hasExplicitNoServiceDefinitionMarkerInterface = true;
            }

            var baseTypeDescriptors = _.GetBaseTypeDescriptors(
                classDeclaration,
                compilationUnit,
                availableDefinitionNamespacedTypeNames);

            var hasSpecifyServiceDefinitionAttribute = Instances.Operation.HasAttributeOfType<T0064.SpecifyServiceDefinitionAttribute>(
                classDeclaration,
                compilationUnit);
            var hasSpecifyServiceDefinitionAttributeValue = hasSpecifyServiceDefinitionAttribute.Exists;
            var hasOldImplementsServiceDefinitionAttribute = Instances.Operation.HasAttributeOfType<T0064.ImplementsServiceDefinitionAttribute>(
                classDeclaration,
                compilationUnit);

            var specifyServiceDefinitionAttribute = hasSpecifyServiceDefinitionAttribute;
            if(!hasSpecifyServiceDefinitionAttributeValue && hasOldImplementsServiceDefinitionAttribute)
            {
                specifyServiceDefinitionAttribute = hasOldImplementsServiceDefinitionAttribute;
                hasSpecifyServiceDefinitionAttributeValue = true;
            }

            var specifyServiceDefinitionType = specifyServiceDefinitionAttribute
                ? _.GetSpecifyServiceDefinitionType(
                    specifyServiceDefinitionAttribute.Result,
                    compilationUnit,
                    availableDefinitionNamespacedTypeNames)
                : Instances.Operation.GetEmptyServiceDefinitionDependencyDescriptor()
                ;

            var constructorDescriptors = _.GetConstructorDescriptors(
                classDeclaration,
                compilationUnit,
                availableDefinitionNamespacedTypeNames);

            var hasServiceImplementationMarkerAttribute = Instances.Operation.HasBaseTypeOfType<T0064.ServiceImplementationMarkerAttribute>(
                classDeclaration,
                compilationUnit);
            var hasServiceImplementationMarkerInterface = Instances.Operation.HasBaseTypeOfType<T0064.IServiceImplementation>(
                classDeclaration,
                compilationUnit)
                .Exists;
            var hasOldServiceImplementationInterface = Instances.Operation.HasBaseTypeOfType<T0064.IServiceImplementation>(
                classDeclaration,
                compilationUnit)
                .Exists;
            if(!hasServiceImplementationMarkerInterface && hasOldServiceImplementationInterface)
            {
                hasServiceImplementationMarkerInterface = true;
            }

            // Create output.
            var output = new ImplementationDescriptor
            {
                NamespacedTypeName = namespacedTypeName,

                IsClass = isClass,
                IsAbstract = isAbstract,
                IsStatic = isStatic,

                HasExplicitNoServiceDefinitionMarkerInterface = hasExplicitNoServiceDefinitionMarkerInterface,
                HasOldNoServiceDefinitionInterface = hasOldNoServiceDefinitionInterface,

                BaseTypeDescriptors = baseTypeDescriptors,

                HasSpecifyServiceDefinitionAttribute = hasSpecifyServiceDefinitionAttributeValue,
                HasOldImplementsServiceDefinitionAttribute = hasOldImplementsServiceDefinitionAttribute,

                SpecifyServiceDefinitionType = specifyServiceDefinitionType,

                ConstructorDescriptors = constructorDescriptors,

                HasServiceImplementationMarkerAttribute = hasServiceImplementationMarkerAttribute,
                HasServiceImplementationMarkerInterface = hasServiceImplementationMarkerInterface,
                HasOldServiceImplementationInterface = hasOldServiceImplementationInterface
            };

            return output;
        }

        public static ParameterDescriptor[] GetParameterDescriptors(this IServiceImplementationOperator _,
            ConstructorDeclarationSyntax constructorDeclaration,
            CompilationUnitSyntax compilationUnitSyntax,
            IList<string> availableDefinitionNamespacedTypeNames)
        {
            var parameters = constructorDeclaration.GetParameters();

            var output = parameters
                .Select(xParameter =>
                {
                    var parameterName = xParameter.GetName();
                    var typeNameFragment = xParameter.Type.GetTypeNameFragment();
                    var hasNotServiceComponentAttribute = Instances.Operation.HasAttributeOfType<T0064.NotServiceComponentAttribute>(
                        xParameter,
                        compilationUnitSyntax);

                    var parameterType = Instances.Operation.GetParameterType_HandleIEnumerable(xParameter);

                    var isRecognizedServiceDefinition = Instances.Operation.TryGuessNamespacedTypeName(
                        availableDefinitionNamespacedTypeNames,
                        compilationUnitSyntax,
                        parameterType);

                    var output = new ParameterDescriptor
                    {
                        ParameterName = parameterName,
                        HasExplicitNotServiceComponentAttribute = hasNotServiceComponentAttribute,
                        IsRecognizedServiceDefinitionType = isRecognizedServiceDefinition.Exists,
                        ServiceDefinitionNamespacedTypeName = isRecognizedServiceDefinition.Result,
                        TypeNameFragment = typeNameFragment,
                    };

                    return output;
                })
                .Now();

            return output;
        }

        public static ConstructorDescriptor[] GetConstructorDescriptors(this IServiceImplementationOperator _,
            ClassDeclarationSyntax classDeclaration,
            CompilationUnitSyntax compilationUnit,
            IList<string> availableDefinitionNamespacedTypeNames)
        {
            var constructors = classDeclaration.GetConstructors();

            var output = constructors
                .Select(xConstructor =>
                {
                    var methodName = xConstructor.GetNamespacedTypeNamedParameterTypedMethodName();
                    var hasConstructorMarkerAttribute = Instances.Operation.HasAttributeOfType<T0064.ServiceImplementationConstructorMarkerAttribute>(
                        xConstructor,
                        compilationUnit);

                    var parameterDescriptors = _.GetParameterDescriptors(
                        xConstructor,
                        compilationUnit,
                        availableDefinitionNamespacedTypeNames);

                    var output = new ConstructorDescriptor
                    {
                        HasServiceImplementationConstructorAttribute = hasConstructorMarkerAttribute,
                        MethodName = methodName,
                        ParameterDescriptors = parameterDescriptors,
                    };

                    return output;
                })
                .Now();

            return output;
        }

        public static BaseTypeDescriptor[] GetBaseTypeDescriptors(this IServiceImplementationOperator _,
            ClassDeclarationSyntax classDeclaration,
            CompilationUnitSyntax compilationUnit,
            IList<string> availableDefinitionNamespacedTypeNames)
        {
            var isServiceDefinitionByBaseType = Instances.Operation.HasBaseTypesOfTypes(
                classDeclaration,
                compilationUnit,
                availableDefinitionNamespacedTypeNames);

            var output = isServiceDefinitionByBaseType
                .Select(x => new BaseTypeDescriptor
                {
                    IsRecognizedServiceDefinitionType = x.Value.Exists,
                    ServiceDefinitionNamespacedTypeName = x.Value.Result,
                    TypeNameFragment = x.Key.Type.GetTypeNameFragment(),
                })
                .Now();

            return output;
        }

        public static ServiceDefinitionDependencyDescriptor GetSpecifyServiceDefinitionType(this IServiceImplementationOperator _,
            AttributeSyntax specifyServiceDefinitionAttribute,
            CompilationUnitSyntax compilationUnit,
            IList<string> availableDefinitionNamespacedTypeNames)
        {
            var attributeHasArguments = specifyServiceDefinitionAttribute.HasArguments();
            if (attributeHasArguments)
            {
                var firstArgument = attributeHasArguments.Result.First();

                if (firstArgument.Expression is TypeOfExpressionSyntax typeOfExpressionSyntax)
                {
                    // Guess the type provided for the typeof operator.
                    var specifiedServiceDefinitionType = Instances.Operation.TryGuessNamespacedTypeName(
                        availableDefinitionNamespacedTypeNames,
                        compilationUnit,
                        typeOfExpressionSyntax.Type);

                    var typeNameFragment = typeOfExpressionSyntax.Type.GetTypeNameFragment();
                    var isRecognizedServiceDefinitionType = specifiedServiceDefinitionType.Exists;
                    var serviceDefinitionNamespacedTypeName = specifiedServiceDefinitionType.Result;

                    var output = new ServiceDefinitionDependencyDescriptor
                    {
                        IsRecognizedServiceDefinitionType = isRecognizedServiceDefinitionType,
                        ServiceDefinitionNamespacedTypeName = serviceDefinitionNamespacedTypeName,
                        TypeNameFragment = typeNameFragment,
                    };

                    return output;
                }
            }
            // Else

            return Instances.Operation.GetEmptyServiceDefinitionDependencyDescriptor();
        }

        public static ImplementationValidationResult Validate(this IServiceImplementationOperator _,
            ImplementationDescriptor implementation)
        {
            var hasExplicitNoServiceDefinitionMarkerButHasDefinitionBaseType = implementation.HasExplicitNoServiceDefinitionMarkerInterface
                && implementation.BaseTypeDescriptors
                    .Where(x => x.IsRecognizedServiceDefinitionType)
                    .Any();

            var hasExplicitNoServiceDefinitionMarkerButHasSpecifyDefinitionAttribute = implementation.HasExplicitNoServiceDefinitionMarkerInterface
                && implementation.HasSpecifyServiceDefinitionAttribute;

            var specifyServiceDefinitionTypeUnrecognized = implementation.HasSpecifyServiceDefinitionAttribute
                && !implementation.SpecifyServiceDefinitionType.IsRecognizedServiceDefinitionType;

            var specifiedServiceDefinitionNotFoundInBaseTypes = implementation.HasSpecifyServiceDefinitionAttribute
                && !implementation.BaseTypeDescriptors
                    .Select(x => x.ServiceDefinitionNamespacedTypeName)
                    .Contains(implementation.SpecifyServiceDefinitionType.ServiceDefinitionNamespacedTypeName)
                ;

            // Only care if no base type is recognized as a service definition if the implementation is not explicitly marked with the no service definition marker interface.
            var noServiceDefinitionBaseTypeRecognized = implementation.BaseTypeDescriptors
                .Where(x => x.IsRecognizedServiceDefinitionType)
                .None()
                && !implementation.HasExplicitNoServiceDefinitionMarkerInterface;

            var multipleServiceDefinitionBaseTypesWithoutMarkerAttribute = !implementation.HasServiceImplementationMarkerAttribute
                && implementation.BaseTypeDescriptors
                    .Where(x => x.IsRecognizedServiceDefinitionType)
                    .Multiple();

            var multipleConstructorsWithoutMarkerAttribute = implementation.ConstructorDescriptors
                .Where(x => !x.HasServiceImplementationConstructorAttribute)
                .Multiple();

            var multipleConstructorsWithMarkerAttribute = implementation.ConstructorDescriptors
                .Where(x => x.HasServiceImplementationConstructorAttribute)
                .Multiple();

            var constructorResults = _.Validate(implementation.ConstructorDescriptors);

            var chosenConstructor = Instances.Operation.ChooseConstructor(
                implementation,
                out var _);

            var chosenConstructorResult = chosenConstructor.Exists
                ? _.Validate(chosenConstructor)
                : new ConstructorValidationResult
                {
                    MethodName = "< Default Constructor >",
                    ParameterResults = Array.Empty<ParameterValidationResult>(),
                }
                ;

            var output = new ImplementationValidationResult
            {
                IsClass = implementation.IsClass,
                IsAbstract = implementation.IsAbstract,
                IsStatic = implementation.IsStatic,

                SpecifyServiceDefinitionTypeUnrecognized = specifyServiceDefinitionTypeUnrecognized,
                SpecifiedServiceDefinitionNotFoundInBaseTypes = specifiedServiceDefinitionNotFoundInBaseTypes,

                NoServiceDefinitionBaseTypeRecognized = noServiceDefinitionBaseTypeRecognized,

                MultipleServiceDefinitionBaseTypesWithoutMarkerAttribute = multipleServiceDefinitionBaseTypesWithoutMarkerAttribute,

                HasExplicitNoServiceDefinitionMarkerButHasDefinitionBaseType = hasExplicitNoServiceDefinitionMarkerButHasDefinitionBaseType,
                HasExplicitNoServiceDefinitionMarkerButHasSpecifyDefinitionAttribute = hasExplicitNoServiceDefinitionMarkerButHasSpecifyDefinitionAttribute,

                MultipleConstructorsWithoutMarkerAttribute = multipleConstructorsWithoutMarkerAttribute,
                MultipleConstructorsWithMarkerAttribute = multipleConstructorsWithMarkerAttribute,

                ConstructorResults = constructorResults,
                ChosenConstructorResult = chosenConstructorResult,

                HasOldImplementsServiceDefinitionAttribute = implementation.HasOldImplementsServiceDefinitionAttribute,
                HasOldNoServiceDefinitionInterface = implementation.HasOldNoServiceDefinitionInterface,
                HasOldServiceImplementationInterface = implementation.HasOldServiceImplementationInterface,
            };

            return output;
        }

        public static ConstructorValidationResult[] Validate(this IServiceImplementationOperator _,
            IEnumerable<ConstructorDescriptor> constructors)
        {
            var output = constructors
                .Select(x => _.Validate(x))
                .Now();

            return output;
        }

        public static ConstructorValidationResult Validate(this IServiceImplementationOperator _,
            ConstructorDescriptor constructor)
        {
            var parameterResults = _.Validate(constructor.ParameterDescriptors);

            var output = new ConstructorValidationResult
            {
                MethodName = constructor.MethodName,
                ParameterResults = parameterResults,
            };

            return output;
        }

        public static ParameterValidationResult[] Validate(this IServiceImplementationOperator _,
            IEnumerable<ParameterDescriptor> parameters)
        {
            var output = parameters
                .Select(x => _.Validate(x))
                .Now();

            return output;
        }

        public static ParameterValidationResult Validate(this IServiceImplementationOperator _,
            ParameterDescriptor parameter)
        {
            var notRecognizedAndMissingExplicitNotServiceComponentAttribute = !parameter.HasExplicitNotServiceComponentAttribute
                && !parameter.IsRecognizedServiceDefinitionType;

            var output = new ParameterValidationResult
            {
                ParameterName = parameter.ParameterName,
                NotRecognizedAndMissingExplicitNotServiceComponentAttribute = notRecognizedAndMissingExplicitNotServiceComponentAttribute,
            };

            return output;
        }
    }
}
