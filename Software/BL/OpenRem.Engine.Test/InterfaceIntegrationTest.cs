using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using NUnit.Framework;
using OpenRem.Common;

namespace OpenRem.Engine.Test
{
    [TestFixture]
    class InterfaceIntegrationTest
    {
        [OneTimeSetUp]
        public void Setup()
        {
            var testAssemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Assembly.LoadFile(Path.Combine(testAssemblyPath, AppDomainHelper.EngineInterfaceAssemblyName + ".dll"));
        }


        [Test]
        public void EveryInterface_Have_ServiceContractAttribute()
        {
            var interfaces = AppDomainHelper.GetInterfaceTypes(AppDomainHelper.EngineInterfaceAssemblyName);

            foreach (var @interface in interfaces)
            {
                var attributes = @interface.GetCustomAttributes(typeof(ServiceContractAttribute), false);
                Assert.IsTrue(attributes.Length > 0, $"{@interface.Namespace}.{@interface.Name}");
            }
        }

        [Test]
        public void EveryInterface_MethodsHave_OperationContractAttribute()
        {
            var interfaces = AppDomainHelper.GetInterfaceTypes(AppDomainHelper.EngineInterfaceAssemblyName);

            foreach (var @interface in interfaces)
            {
                var methods = @interface.GetMethods();
                foreach (var method in methods)
                {
                    var attributes = method.GetCustomAttributes(typeof(OperationContractAttribute), false);
                    Assert.IsTrue(attributes.Length > 0, $"{@interface.Namespace}.{@interface.Name}.{method.Name}");
                }
            }
        }

        [Test]
        public void EveryReferenceType_Have_ParameterlessConstructor()
        {
            var referenceTypes = AppDomainHelper.GetReferenceTypes(AppDomainHelper.EngineInterfaceAssemblyName);

            foreach (var referenceType in referenceTypes)
            {
                var constructors = referenceType.GetConstructors();
                var parameterlessCount = constructors.Count(x => x.GetParameters().Length == 0);
                Assert.AreEqual(1, parameterlessCount, $"{referenceType.Namespace}.{referenceType.Name}");
            }
        }

        [Test]
        public void EveryReferenceType_Have_DataContractAttribute()
        {
            var referenceTypes = AppDomainHelper.GetReferenceTypes(AppDomainHelper.EngineInterfaceAssemblyName);

            foreach (var referenceType in referenceTypes)
            {
                var attributes = referenceType.GetCustomAttributes(typeof(DataContractAttribute), false);
                Assert.IsTrue(attributes.Length > 0, $"{referenceType.Namespace}.{referenceType.Name}");
            }
        }

        [Test]
        public void EveryReferenceType_PropertiesHave_DataMember()
        {
            var referenceTypes = AppDomainHelper.GetReferenceTypes(AppDomainHelper.EngineInterfaceAssemblyName);

            foreach (var referenceType in referenceTypes)
            {
                var properties = referenceType.GetProperties();

                foreach (var property in properties)
                {
                    var attributes = property.GetCustomAttributes(typeof(DataMemberAttribute), false);
                    Assert.IsTrue(attributes.Length > 0, $"{referenceType.Namespace}.{referenceType.Name}.{property.Name}");
                }
            }
        }
    }
}
