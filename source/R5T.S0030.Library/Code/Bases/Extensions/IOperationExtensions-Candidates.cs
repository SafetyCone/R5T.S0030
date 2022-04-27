using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar.Results;

using R5T.T0098;

using R5T.S0030.T002.ImplementationCandidates.Level01;


namespace R5T.S0030.Library
{
    public static partial class IOperationExtensions
    {
        public static List<ClassDeclarationSyntax> GetCandidateClasses(this IOperation _,
            CompilationUnitSyntax compilationUnit,
            out TypeUpgradeResult[] failedCandidates)
        {
            var types = compilationUnit.GetTypes();

            var typesByNamespacedTypeName = types
                .ToDictionary(
                    x => x.GetNamespacedTypeName_HandlingTypeParameters());

            var failedCandidatesList = new List<TypeUpgradeResult>();
            var candidateClasses = new List<ClassDeclarationSyntax>();

            foreach (var pair in typesByNamespacedTypeName)
            {
                var typeDescriptor = Instances.Operation.Describe(pair.Value);

                var upgradeResult = Instances.Operation.Upgrade(
                    typeDescriptor,
                    out var upgradedTypeDescriptor);

                var isSuccess = upgradeResult.ValidationSuccess.Succeeded();
                if (isSuccess)
                {
                    // Type is know to be a class.
                    candidateClasses.Add(pair.Value as ClassDeclarationSyntax);
                }
                else
                {
                    failedCandidatesList.Add(upgradeResult);
                }
            }

            failedCandidates = failedCandidatesList.ToArray();

            return candidateClasses;
        }

        public static TypeDescriptor Describe(this IOperation _,
            TypeDeclarationSyntax typeDeclaration)
        {
            var namespacedTypeName = typeDeclaration.GetNamespacedTypeName_HandlingTypeParameters();

            var isClass = typeDeclaration.IsClass();
            var isAbstract = typeDeclaration.IsAbstract();
            var isStatic = typeDeclaration.IsStatic();

            var output = new TypeDescriptor
            {
                NamespacedTypeName = namespacedTypeName,

                IsClass = isClass,
                IsAbstract = isAbstract,
                IsStatic = isStatic,
            };

            return output;
        }

        public static TypeValidationResult Validate(this IOperation _,
            TypeDescriptor typeDescriptor)
        {
            var output = new TypeValidationResult
            {
                NamespacedTypeName = typeDescriptor.NamespacedTypeName,

                IsClass = typeDescriptor.IsClass,
                IsAbstract = typeDescriptor.IsAbstract,
                IsStatic = typeDescriptor.IsStatic,
            };

            return output;
        }

        public static MultipleActionResult IsSuccess(this IOperation _,
            TypeValidationResult validationResult)
        {
            var actionResults = new List<ActionResult>();

            if(!validationResult.IsClass)
            {
                actionResults.Add(
                    ActionResult.Failure("Type must be a class."));
            }

            if (validationResult.IsAbstract)
            {
                actionResults.Add(
                    ActionResult.Failure("Type must not be abstract."));
            }

            if (validationResult.IsStatic)
            {
                actionResults.Add(
                    ActionResult.Failure("Type must not be static."));
            }

            var output = new MultipleActionResult(actionResults);
            return output;
        }

        public static TypeUpgradeResult Upgrade(this IOperation _,
            TypeDescriptor typeDescriptor,
            out T002.ImplementationCandidates.Level02.TypeDescriptor upgradedTypeDescriptor)
        {
            var validationResult = _.Validate(typeDescriptor);

            var validationSuccess = _.IsSuccess(validationResult);

            var isSuccess = validationSuccess.Succeeded();
            if (isSuccess)
            {
                upgradedTypeDescriptor = new T002.ImplementationCandidates.Level02.TypeDescriptor
                {
                    NamespacedTypeName = typeDescriptor.NamespacedTypeName,
                };
            }
            else
            {
                upgradedTypeDescriptor = null;
            }

            var output = new TypeUpgradeResult
            {
                NamespacedTypeName = typeDescriptor.NamespacedTypeName,

                ValidationResult = validationResult,
                ValidationSuccess = validationSuccess,
            };

            return output;
        }
    }
}
