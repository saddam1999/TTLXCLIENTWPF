﻿Reflector.Disassembler.Translator+ExpressionHandlingException: Expression stack is empty at offset 00C6.
   在 Reflector.Disassembler.Translator.PeekExpression()
   在 Reflector.Disassembler.Translator.DecodeDuplicateStatement(Int32 offset, IEnumerable`1& AdditionalExpressions)
   在 Reflector.Disassembler.Translator.DecodeStatement(Int32 end, IEnumerable`1& AdditionalExpressions)
   在 Reflector.Disassembler.Translator.DecodeBlockStatement(Int32 offset, Int32 end)
   在 Reflector.Disassembler.Translator.TranslateMethodDeclaration(IMethodDeclaration mD, IMethodBody mB, Boolean handleExpressionStack)
   在 Reflector.Disassembler.Translator.TranslateMethodDeclaration(IMethodDeclaration mD, IMethodBody mB)
   在 Reflector.Disassembler.Optimizations.InnerMethodOptimizer.TranslateAnonymousMethodExpression(ITypeReference delegateType, IMethodReference methodReference, Int32 ilOffset, ThisDisplayClassContext thisContext)
   在 Reflector.Disassembler.Optimizations.InnerMethodOptimizer.TranslateDelegateCreateExpression(IDelegateCreateExpression value)
   在 Reflector.Disassembler.Optimizations.InnerMethodOptimizer.TransformDelegateCreateExpression(IDelegateCreateExpression value)
   在 Reflector.CodeModel.Visitor.Transformer.TransformExpression(IExpression value)
   在 Reflector.CodeModel.Visitor.Transformer.TransformAttachEventStatement(IAttachEventStatement value)
   在 Reflector.CodeModel.Visitor.Transformer.TransformStatement(IStatement value)
   在 Reflector.Disassembler.Optimizations.InnerMethodOptimizer.TransformDisplayClass(IBlockStatement value)
   在 Reflector.Disassembler.Optimizations.InnerMethodOptimizer.TransformBlockStatement(IBlockStatement value)
   在 Reflector.CodeModel.Visitor.Transformer.TransformStatement(IStatement value)
   在 Reflector.CodeModel.Visitor.Transformer.TransformMethodDeclaration(IMethodDeclaration value)
   在 Reflector.Disassembler.Optimizations.ContextTransformer.TransformMethodDeclaration(IMethodDeclaration value)
   在 Reflector.Disassembler.Optimizations.InnerMethodOptimizer.TransformMethodDeclaration(IMethodDeclaration value)
   在 Reflector.Disassembler.Optimizer.TransformMethodDeclaration(IMethodDeclaration value)
   在 Reflector.Disassembler.Disassembler.TransformMethodDeclaration(IMethodDeclaration value)
   在 Reflector.CodeModel.Visitor.Transformer.TransformMethodDeclarationCollection(IMethodDeclarationCollection methods)
   在 Reflector.Disassembler.Disassembler.TransformTypeDeclaration(ITypeDeclaration value)
   在 Reflector.Application.Translator.TranslateTypeDeclaration(ITypeDeclaration value, Boolean memberDeclarationList, Boolean methodDeclarationBody)
   在 Reflector.Application.FileDisassembler.WriteTypeDeclaration(ITypeDeclaration typeDeclaration, String sourceFile, ILanguageWriterConfiguration languageWriterConfiguration)
namespace TTLX.WindowsTool.Common.Controls
{
}

