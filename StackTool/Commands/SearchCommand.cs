using EnvDTE;
using Microsoft.VisualStudio.Shell;
using System;
using System.ComponentModel.Design;

namespace StackTool.Commands
{
    public class SearchCommand
    {
        private readonly IServiceProvider package;

        protected SearchCommand(Package package)
        {
            if (package == null)
            {
                throw new ArgumentNullException("package");
            }

            this.package = package;

            OleMenuCommandService commandService = this.ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (commandService != null)
            {
                var bingCommandId = new CommandID(Constant.PackageCommandSetGuid, Constant.BingCommandId);
                var bingMenuItem = new MenuCommand(MenuItemClick, bingCommandId);
                commandService.AddCommand(bingMenuItem);

                var docsCommandId = new CommandID(Constant.PackageCommandSetGuid, Constant.DocsCommandId);
                var docsMenuItem = new MenuCommand(MenuItemClick, docsCommandId);
                commandService.AddCommand(docsMenuItem);
            }
        }

        public static SearchCommand Instance { get; private set; }
        protected IServiceProvider ServiceProvider => package;

        public static void Initialize(Package package)
        {
            Instance = new SearchCommand(package);
        }

        private void MenuItemClick(object sender, EventArgs e)
        {
            string url = string.Empty;

            var commnad = sender as MenuCommand;
            if (commnad != null && commnad.CommandID != null)
            {
                string keyword = string.Empty;
                var dte = ServiceProvider.GetService(typeof(DTE)) as DTE;
                if (dte.ActiveDocument != null && dte.ActiveDocument.Selection != null)
                {
                    var text = dte.ActiveDocument.Selection as TextSelection;
                    keyword = text.Text;
                }

                if (commnad.CommandID.ID == Constant.BingCommandId)
                {
                    url = !string.IsNullOrWhiteSpace(keyword) ? $"http://www.bing.com/search?q={keyword}" : "http://www.bing.com";
                }
                else if (commnad.CommandID.ID == Constant.DocsCommandId)
                {
                    url = !string.IsNullOrWhiteSpace(keyword) ? $"https://docs.microsoft.com/en-us/search/index?search={keyword}" : "http://docs.microsoft.com";
                }
            }

            System.Diagnostics.Process.Start(url);
        }
    }
}