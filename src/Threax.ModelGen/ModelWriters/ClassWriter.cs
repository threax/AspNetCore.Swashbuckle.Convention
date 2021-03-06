﻿using System;
using System.Collections.Generic;
using System.Text;
using Threax.ModelGen.ModelWriters;

namespace Threax.ModelGen
{
    public class ClassWriter : ITypeWriter
    {
        private bool hasCreated = false;
        private bool hasModified = false;
        protected IAttributeBuilder PropAttrBuilder { get; private set; }
        protected IAttributeBuilder ClassAttrBuilder { get; private set; }

        public ClassWriter(bool hasCreated, bool hasModified, IAttributeBuilder propAttrBuilder, IAttributeBuilder classAttrBuilder = null)
        {
            this.hasCreated = hasCreated;
            this.hasModified = hasModified;
            this.PropAttrBuilder = propAttrBuilder;
            this.ClassAttrBuilder = classAttrBuilder;
        }

        public virtual void AddUsings(StringBuilder sb, String ns)
        {
            if (WriteUsings)
            {
                sb.AppendLine(
    @"using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Halcyon.HAL.Attributes;
using Threax.AspNetCore.Halcyon.Ext;
using Threax.AspNetCore.Models;"
                );

                if (hasCreated || hasModified)
                {
                    sb.AppendLine("using Threax.AspNetCore.Tracking;");
                }

                if (!String.IsNullOrEmpty(AdditionalUsings))
                {
                    sb.AppendLine(AdditionalUsings);
                }
            }
        }

        public String AdditionalUsings { get; set; }

        public bool WriteUsings { get; set; } = true;

        public virtual void StartType(StringBuilder sb, String name, String pluralName)
        {
            if (WriteType)
            {
                ClassAttrBuilder?.BuildAttributes(sb, name, new NoWriterInfo(), "    ");

                sb.AppendLine(
    $@"    public {GetInheritance()}class {name}{InterfaceListBuilder.Build(GetAdditionalInterfaces())}
    {{"
                );
            }
        }

        public virtual void EndType(StringBuilder sb, String name, String pluralName)
        {
            if (WriteType)
            {
                if (hasCreated)
                {
                    CreateProperty(sb, "Created", new TypeWriterPropertyInfo<DateTime>()
                    {
                        Order = int.MaxValue - 1
                    });
                }

                if (hasModified)
                {
                    CreateProperty(sb, "Modified", new TypeWriterPropertyInfo<DateTime>()
                    {
                        Order = int.MaxValue
                    });
                }

                sb.AppendLine("    }");
            }
        }

        public bool WriteType { get; set; } = true;

        public virtual void CreateProperty(StringBuilder sb, String name, IWriterPropertyInfo info)
        {
            if (WriteProperties)
            {
                PropAttrBuilder.BuildAttributes(sb, name, info, "        ");
                sb.AppendLine($"        public {GetInheritance()}{info.ClrType} {name} {{ get; set; }}");
                sb.AppendLine();
            }
        }

        public bool WriteProperties { get; set; } = true;

        public bool WriteAsAbstractClass { get; set; } = false;

        public virtual void EndNamespace(StringBuilder sb)
        {
            if (WriteNamespace)
            {
                sb.AppendLine("}");
            }
        }

        public virtual void StartNamespace(StringBuilder sb, string name)
        {
            if (WriteNamespace)
            {
                sb.AppendLine();
                sb.AppendLine(
$@"namespace {name} 
{{"
                );
            }
        }

        public bool WriteNamespace { get; set; } = true;

        public IEnumerable<String> GetAdditionalInterfaces()
        {
            if (hasCreated && hasModified)
            {
                yield return "ICreatedModified";
            }
            else
            {
                if (hasCreated)
                {
                    yield return "ICreated";
                }

                if (hasModified)
                {
                    yield return "IModified";
                }
            }
        }

        protected virtual string GetInheritance()
        {
            var inheritance = "";
            if (WriteAsAbstractClass)
            {
                inheritance = "abstract ";
            }

            return inheritance;
        }
    }
}
