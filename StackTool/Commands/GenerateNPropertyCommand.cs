using EnvDTE;
using Microsoft.VisualStudio.Shell;
using System;
using System.ComponentModel.Design;
using System.Text;
using System.Windows;

namespace StackTool.Commands
{
    public class GenerateNPropertyCommand
    {
        private readonly System.IServiceProvider package;

        public GenerateNPropertyCommand(Package package)
        {
            this.package = package;

            OleMenuCommandService commandService = this.ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (commandService != null)
            {
                var cmdId = new CommandID(Constant.PackageCommandSetGuid, Constant.GenerateNPropertyCommandId);
                var cmd = new MenuCommand(MenuItemClick, cmdId);
                commandService.AddCommand(cmd);
            }
        }

        public static GenerateNPropertyCommand Instance { get; private set; }

        private IServiceProvider ServiceProvider => package;

        public static void Initialize(Package package)
        {
            Instance = new GenerateNPropertyCommand(package);
        }

        private void MenuItemClick(object sender, EventArgs e)
        {
            string context = string.Empty;
            var dte = ServiceProvider.GetService(typeof(DTE)) as DTE;
            if (dte.ActiveDocument != null && dte.ActiveDocument.Selection != null)
            {
                var text = dte.ActiveDocument.Selection as TextSelection;
                context = text.Text.Trim();
            }

            // "string Name"
            var parts = new string[] { };

            parts = context.Split(' ');

            if (parts.Length == 0)
            {
                return;
            }

            var varName = parts[parts.Length - 1].Trim();
            var localVarName = parts[parts.Length - 1].Trim();
            if (localVarName.Length > 1)
            {
                localVarName = "_" + char.ToLower(localVarName[0]) + localVarName.Substring(1);
            }
            else
            {
                localVarName += "_";
            }

            StringBuilder sb = new StringBuilder();
            var type = string.Empty;
            if (parts.Length > 1)
            {
                type = parts[0];
            }
            else
            {
                type = "string";
            }

            sb.AppendLine($"private {type} {localVarName};");
            sb.AppendLine();
            sb.AppendLine($"public {type} {varName}");
            sb.AppendLine("{");
            sb.AppendLine("    get { return " + localVarName + "; }");
            sb.AppendLine("    set { Set(ref " + localVarName + ", value); }");
            sb.AppendLine("}");
            Clipboard.SetText(sb.ToString());
        }
    }
}