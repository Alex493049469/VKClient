using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VK.ViewModel.Dialogs.DataTemplateSelector
{
    class DialogDataTemplateSelector : System.Windows.Controls.DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;

            if (element != null && item != null && item is DialogItemViewModel)
            {
                DialogItemViewModel dialog = item as DialogItemViewModel;

                if (dialog.GroupPhoto100 != null)
                {
                    return element.FindResource("Group") as DataTemplate;
                }
                if (dialog.ChatActive == null || dialog.UserCount == null)
                {
                    return element.FindResource("One") as DataTemplate;
                }
                if (dialog.ChatActive.Count == 2)
                {
                    return element.FindResource("Two") as DataTemplate;
                }
                if (dialog.ChatActive.Count == 3)
                {
                    return element.FindResource("Three") as DataTemplate;
                }

                if (dialog.ChatActive.Count >= 4)
                {
                    return element.FindResource("Four") as DataTemplate;
                }
            }

            return null;
        }
    }
}
