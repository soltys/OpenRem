using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace OpenRem.CommonUI
{
    /// <summary>
    /// Behavior that allows to attach command to any <see cref="FrameworkElement"/> and use Command pattern in a proper way (Button a'like).
    /// <para/> Behavior intended to use inside DataTemplates for <see cref="UICommand"/>.
    /// <para/> It will also update 'IsEnabled' state to correspond to the command CanExecute state whenever command raises its CanExecuteStateChanged event.
    /// <para/> Notes:
    /// <para/> 1: It should not be used directly in the View, only inside style templates. Otherwise it might not work correctly when changing language/style.
    /// <para/> 2: For "Control" based elements, command will be invoked on "Click" event. On other FrameworkElements it will be invoked on "MouseUp" event.
    /// <para/> 3: When 'AssociatedObject' is Unloaded, it will unsubscribe from all event handlers except the Loaded and Unloaded events. 
    /// <para/> This event will be kept as a WeakEvent to allow element to be unloaded and loaded again (like when ComboBox dropdown is opened and closed).
    /// <para/> 4: It subscribes to the Command's CanExecuteChanged event whenever command changes (and unsubscribes from the old command event). 
    /// </summary>
    /// <example>
    /// <i:Interaction.Behaviors>
    ///    <commonUi:CommandBehavior Command="{Binding OkCommand}" CommandParameter="{Binding OkCommandParameter}" />
    /// </i:Interaction.Behaviors>
    /// </example> 
    public class CommandBehavior : Behavior<FrameworkElement>
    {
        #region Command property

        /// <summary>
        /// Command to be invoked when Click (for 'Control' based elements) or MouseUp (for non 'Control' based elements) occurs. 
        /// </summary>
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            "Command",
            typeof(ICommand),
            typeof(CommandBehavior),
            new FrameworkPropertyMetadata(null, OnCommandChanged));

        /// <summary>
        /// Gets or sets the Command associated with this behavior.
        /// </summary>
        public ICommand Command
        {
            get { return (ICommand) GetValue(CommandBehavior.CommandProperty); }
            set { SetValue(CommandBehavior.CommandProperty, value); }
        }

        private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var cmdBehavior = (CommandBehavior) d;
            cmdBehavior.UnsubscribeCommandEvent(e.OldValue as ICommand);
            cmdBehavior.SubscribeCommandEvent(e.NewValue as ICommand);
        }

        #endregion Command property

        #region CommandParameter property

        /// <summary>
        /// Allows to add additional parameter to the command when it is executed or it's CanExecute state is evaluated.
        /// </summary>
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(
            "CommandParameter",
            typeof(object),
            typeof(CommandBehavior));

        /// <summary>
        /// Gets or sets Command's parameter passed whenever command is executed or CanExecute state is evaluated.
        /// </summary>
        public object CommandParameter
        {
            get { return GetValue(CommandBehavior.CommandParameterProperty); }
            set { SetValue(CommandBehavior.CommandParameterProperty, value); }
        }

        #endregion CommandParameter property

        protected override void OnAttached()
        {
            base.OnAttached();
            WeakEventManager<FrameworkElement, RoutedEventArgs>.AddHandler(AssociatedObject, "Loaded", OnLoaded);
            WeakEventManager<FrameworkElement, RoutedEventArgs>.AddHandler(AssociatedObject, "Unloaded", OnUnloaded);
            SubscibeClickHandler();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            UnsubscribeCommandEvent(Command);
            SubscribeCommandEvent(Command);
            UnsubscribeClickHandler();
            SubscibeClickHandler();
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            UnsubscribeCommandEvent(Command);
            UnsubscribeClickHandler();
        }

        private void UnsubscribeClickHandler()
        {
            if (AssociatedObject is TextBox textBox)
            {
                WeakEventManager<TextBox, RoutedEventArgs>.RemoveHandler(textBox, "GotFocus", OnGotFocus);
                return;
            }

            if (AssociatedObject is ButtonBase buttonBase)
            {
                WeakEventManager<ButtonBase, MouseButtonEventArgs>.RemoveHandler(buttonBase, "Click", OnClick);
            }
            else
            {
                WeakEventManager<FrameworkElement, MouseButtonEventArgs>.RemoveHandler(AssociatedObject, "MouseUp",
                    OnMouseUp);
            }
        }

        private void SubscibeClickHandler()
        {
            if (AssociatedObject is TextBox textBox)
            {
                WeakEventManager<TextBox, RoutedEventArgs>.AddHandler(textBox, "GotFocus", OnGotFocus);
                return;
            }

            if (AssociatedObject is ButtonBase buttonBase)
            {
                WeakEventManager<ButtonBase, MouseButtonEventArgs>.AddHandler(buttonBase, "Click", OnClick);
            }
            else
            {
                WeakEventManager<FrameworkElement, MouseButtonEventArgs>.AddHandler(AssociatedObject, "MouseUp",
                    OnMouseUp);
            }
        }

        private void OnClick(object sender, MouseButtonEventArgs eventargs)
        {
            ExecuteCommand();
        }

        private void OnGotFocus(object sender, RoutedEventArgs e)
        {
            ExecuteCommand();
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            ExecuteCommand();
        }

        private void ExecuteCommand()
        {
            var cmd = Command;
            if (cmd != null && cmd.CanExecute(CommandParameter))
            {
                cmd.Execute(CommandParameter);
            }
        }

        private void SubscribeCommandEvent(ICommand command)
        {
            if (command != null)
            {
                command.CanExecuteChanged += OnCanExecuteChanged;
                OnCanExecuteChanged(this, EventArgs.Empty);
            }
        }

        private void UnsubscribeCommandEvent(ICommand command)
        {
            if (command != null)
            {
                command.CanExecuteChanged -= OnCanExecuteChanged;
            }
        }

        private void OnCanExecuteChanged(object sender, EventArgs e)
        {
            var cmd = Command;
            var ao = AssociatedObject;
            if (ao != null)
            {
                ao.IsEnabled = cmd == null || cmd.CanExecute(CommandParameter);
            }
        }
    }
}