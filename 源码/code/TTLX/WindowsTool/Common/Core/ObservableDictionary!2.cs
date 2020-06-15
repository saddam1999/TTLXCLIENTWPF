Reflector.Disassembler.Translator+ExpressionHandlingException: Expression stack is empty at offset 0007.
   在 Reflector.Disassembler.Translator.PeekExpression()
   在 Reflector.Disassembler.Translator.DecodeDuplicateStatement(Int32 offset, IEnumerable`1& AdditionalExpressions)
   在 Reflector.Disassembler.Translator.DecodeStatement(Int32 end, IEnumerable`1& AdditionalExpressions)
   在 Reflector.Disassembler.Translator.DecodeBlockStatement(Int32 offset, Int32 end)
   在 Reflector.Disassembler.Translator.TranslateMethodDeclaration(IMethodDeclaration mD, IMethodBody mB, Boolean handleExpressionStack)
   在 Reflector.Disassembler.Translator.TranslateMethodDeclaration(IMethodDeclaration mD, IMethodBody mB)
   在 Reflector.Disassembler.Disassembler.TransformMethodDeclaration(IMethodDeclaration value)
   在 Reflector.CodeModel.Visitor.Transformer.TransformMethodDeclarationCollection(IMethodDeclarationCollection methods)
   在 Reflector.Disassembler.Disassembler.TransformTypeDeclaration(ITypeDeclaration value)
   在 Reflector.Application.Translator.TranslateTypeDeclaration(ITypeDeclaration value, Boolean memberDeclarationList, Boolean methodDeclarationBody)
   在 Reflector.Application.FileDisassembler.WriteTypeDeclaration(ITypeDeclaration typeDeclaration, String sourceFile, ILanguageWriterConfiguration languageWriterConfiguration)
namespace TTLX.WindowsTool.Common.Core
{
}

