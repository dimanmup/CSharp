using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace Reflection_MyTypeBuilder
{
    public class MyTypeBuilder
    {
        public static Type CreateType(string assemblyName, IDictionary<string, Type> fields)
        {
            TypeBuilder tb = GetTypeBuilder(assemblyName);
            ConstructorBuilder constructor = tb.DefineDefaultConstructor(
                MethodAttributes.Public
                | MethodAttributes.SpecialName
                | MethodAttributes.RTSpecialName);

            foreach (var field in fields)
            {
                CreateProperty(tb, field.Key, field.Value);
            }

            Type objectType = tb.CreateType();

            return objectType;
        }

        public static object CreateInstance(string assemblyName, IDictionary<string, Type> fields)
        {
            var myType = CreateType(assemblyName, fields);
            var myObject = Activator.CreateInstance(myType);

            return myObject;
        }

        private static TypeBuilder GetTypeBuilder(string typeSignature)
        {
            AssemblyName an = new AssemblyName(typeSignature);
            AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(an, AssemblyBuilderAccess.Run);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("MainModule");
            TypeBuilder tb = moduleBuilder.DefineType(typeSignature,
                    TypeAttributes.Public |
                    TypeAttributes.Class |
                    TypeAttributes.AutoClass |
                    TypeAttributes.AnsiClass |
                    TypeAttributes.BeforeFieldInit |
                    TypeAttributes.AutoLayout,
                    null);
            return tb;
        }

        private static void CreateProperty(TypeBuilder tb, string propertyName, Type propertyType)
        {
            FieldBuilder fieldBuilder = tb.DefineField("_" + propertyName, propertyType, FieldAttributes.Private);

            PropertyBuilder propertyBuilder = tb.DefineProperty(propertyName, PropertyAttributes.HasDefault, propertyType, null);
            MethodBuilder getPropMthdBldr = tb.DefineMethod("get_" + propertyName,
                MethodAttributes.Public
                | MethodAttributes.SpecialName
                | MethodAttributes.HideBySig,
                propertyType, Type.EmptyTypes);
            ILGenerator getIl = getPropMthdBldr.GetILGenerator();

            getIl.Emit(OpCodes.Ldarg_0);
            getIl.Emit(OpCodes.Ldfld, fieldBuilder);
            getIl.Emit(OpCodes.Ret);

            MethodBuilder setPropMthdBldr =
                tb.DefineMethod("set_" + propertyName,
                  MethodAttributes.Public |
                  MethodAttributes.SpecialName |
                  MethodAttributes.HideBySig,
                  null, new[] { propertyType });

            ILGenerator setIl = setPropMthdBldr.GetILGenerator();
            Label modifyProperty = setIl.DefineLabel();
            Label exitSet = setIl.DefineLabel();

            setIl.MarkLabel(modifyProperty);
            setIl.Emit(OpCodes.Ldarg_0);
            setIl.Emit(OpCodes.Ldarg_1);
            setIl.Emit(OpCodes.Stfld, fieldBuilder);

            setIl.Emit(OpCodes.Nop);
            setIl.MarkLabel(exitSet);
            setIl.Emit(OpCodes.Ret);

            propertyBuilder.SetGetMethod(getPropMthdBldr);
            propertyBuilder.SetSetMethod(setPropMthdBldr);
        }
    }
}
