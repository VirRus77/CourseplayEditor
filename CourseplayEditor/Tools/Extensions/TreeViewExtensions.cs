using System.Windows.Controls;

namespace CourseplayEditor.Tools.Extensions
{
    /// <summary>
    /// https://social.msdn.microsoft.com/Forums/silverlight/en-US/84cd3a27-6b17-48e6-8f8a-e5737601fdac/treeviewitemcontainergeneratorcontainerfromitem-returns-null?forum=silverlightnet
    /// </summary>
    public static class TreeViewExtensions

    {
        public static TreeViewItem ContainerFromItem(this TreeView treeView, object item)
        {
            var containerThatMightContainItem = (TreeViewItem) treeView.ItemContainerGenerator.ContainerFromItem(item);

            if (containerThatMightContainItem != null)
            {
                return containerThatMightContainItem;
            }
            else
            {
                return ContainerFromItem(treeView.ItemContainerGenerator, treeView.Items, item);
            }
        }


        private static TreeViewItem ContainerFromItem(
            ItemContainerGenerator parentItemContainerGenerator,
            ItemCollection itemCollection,
            object item
        )
        {
            foreach (var curChildItem in itemCollection)
            {
                var parentContainer = (TreeViewItem) parentItemContainerGenerator.ContainerFromItem(curChildItem);
                if (parentContainer == null)
                {
                    continue;
                }

                var containerThatMightContainItem = (TreeViewItem) parentContainer.ItemContainerGenerator.ContainerFromItem(item);
                if (containerThatMightContainItem != null)
                {
                    return containerThatMightContainItem;
                }

                var recursionResult = ContainerFromItem(parentContainer.ItemContainerGenerator, parentContainer.Items, item);
                if (recursionResult != null)
                {
                    return recursionResult;
                }
            }

            return null;
        }


        public static object ItemFromContainer(this TreeView treeView, TreeViewItem container)
        {
            var itemThatMightBelongToContainer = (TreeViewItem) treeView.ItemContainerGenerator.ItemFromContainer(container);
            if (itemThatMightBelongToContainer != null)
            {
                return itemThatMightBelongToContainer;
            }
            else
            {
                return ItemFromContainer(treeView.ItemContainerGenerator, treeView.Items, container);
            }
        }


        private static object ItemFromContainer(
            ItemContainerGenerator parentItemContainerGenerator,
            ItemCollection itemCollection,
            TreeViewItem container
        )
        {
            foreach (var curChildItem in itemCollection)
            {
                var parentContainer = (TreeViewItem) parentItemContainerGenerator.ContainerFromItem(curChildItem);
                var itemThatMightBelongToContainer =
                    (TreeViewItem) parentContainer.ItemContainerGenerator.ItemFromContainer(container);

                if (itemThatMightBelongToContainer != null)
                {
                    return itemThatMightBelongToContainer;
                }

                var recursionResult =
                    ItemFromContainer(parentContainer.ItemContainerGenerator, parentContainer.Items, container) as TreeViewItem;
                if (recursionResult != null)
                {
                    return recursionResult;
                }
            }

            return null;
        }
    }
}
