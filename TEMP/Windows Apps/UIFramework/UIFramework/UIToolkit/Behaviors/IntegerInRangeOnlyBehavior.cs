using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace UIToolkit
{

    /// <summary>
    /// The Behavior controlling a numeric (integer) input box with the range check.
    /// Optionally it supports the value increasing/decreasing functionality via the Up/Down arrow keys.
    /// </summary>
    public class IntegerInRangeOnlyBehavior : Behavior<TextBox>
    {
        #region Dependency properties

        /// <summary>
        /// The minimum allowed value
        /// </summary>
        public static readonly DependencyProperty MinProperty =
            DependencyProperty.Register("Min", typeof(int), typeof(IntegerInRangeOnlyBehavior),
                                        new FrameworkPropertyMetadata(int.MinValue));

        /// <summary>
        /// The maximum allowed value
        /// </summary>
        public static readonly DependencyProperty MaxProperty =
            DependencyProperty.Register("Max", typeof(int), typeof(IntegerInRangeOnlyBehavior),
                                        new FrameworkPropertyMetadata(int.MaxValue));

        /// <summary>
        /// Handling mode of the arrow key controls
        /// </summary>
        public static readonly DependencyProperty EnableUpDownControlProperty =
            DependencyProperty.Register("EnableUpDownControl", typeof(bool),
            typeof(IntegerInRangeOnlyBehavior), new FrameworkPropertyMetadata(false));

        /// <summary>
        /// A command executed after every change of the text (receiving the new text as an argument)
        /// </summary>
        public static readonly DependencyProperty InputChangedCommandProperty =
            DependencyProperty.Register("InputChangedCommand", typeof(ICommand), typeof(IntegerInRangeOnlyBehavior));

        public int Min
        {
            get { return (int)GetValue(MinProperty); }
            set { SetValue(MinProperty, value); }
        }

        public int Max
        {
            get { return (int)GetValue(MaxProperty); }
            set { SetValue(MaxProperty, value); }
        }

        public bool EnableUpDownControl
        {
            get { return (bool)GetValue(EnableUpDownControlProperty); }
            set { SetValue(EnableUpDownControlProperty, value); }
        }

        public ICommand InputChangedCommand
        {
            get { return (ICommand)GetValue(InputChangedCommandProperty); }
            set { SetValue(InputChangedCommandProperty, value); }
        }

        #endregion

        private bool commandAttached;

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PreviewTextInput += OnPreviewTextInput;
            AssociatedObject.PreviewKeyDown += OnPreviewKeyDown;
            DataObject.AddPastingHandler(AssociatedObject, OnClipboardPaste);
            commandAttached = (null != InputChangedCommand);
            if (commandAttached)
                AssociatedObject.TextChanged += OnTextChanged;
        }

        protected override void OnDetaching()
        {
            if (commandAttached)
                AssociatedObject.TextChanged -= OnTextChanged;

            DataObject.RemovePastingHandler(AssociatedObject, OnClipboardPaste);
            AssociatedObject.PreviewKeyDown -= OnPreviewKeyDown;
            AssociatedObject.PreviewTextInput -= OnPreviewTextInput;
            base.OnDetaching();
        }

        private int Value
        {
            get
            {
                string text = AssociatedObject.Text;
                return string.IsNullOrEmpty(text) ? 0 : int.Parse(text, CultureInfo.CurrentCulture);
            }
        }

        private void OnClipboardPaste(object sender, DataObjectPastingEventArgs dopea)
        {
            string text = dopea.SourceDataObject.GetData(dopea.FormatToApply).ToString();
            if (!this.Validate(text))
                dopea.CancelCommand();
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            ICommand command = InputChangedCommand;
            string text = AssociatedObject.Text;
            if (command.CanExecute(text))
                command.Execute(text);
        }

        private void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !this.Validate(e.Text);
        }

        private bool Validate(string text)
        {
            string oldText = AssociatedObject.Text;
            int caret = AssociatedObject.CaretIndex;
            int selectionLength = AssociatedObject.SelectionLength;

            if (selectionLength > 0)
            {
                int selectionStart = AssociatedObject.SelectionStart;
                oldText = oldText.Remove(selectionStart, selectionLength);
                caret = selectionStart;
            }

            string newText = oldText.Insert(caret, text);
            if (this.Min < 0 && newText.Equals(NumberFormatInfo.CurrentInfo.NegativeSign))
            {
                return true;
            }

            int value;
            return int.TryParse(newText, out value) ? ((value >= Min) && (value <= Max)) : false;
        }

        private void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (EnableUpDownControl)
            {
                if (e.Key == Key.Up)
                {
                    int newValue = Value + 1;
                    if (newValue <= Max)
                        AssociatedObject.Text = newValue.ToString(CultureInfo.CurrentCulture);
                    e.Handled = true;
                    return;
                }

                if (e.Key == Key.Down)
                {
                    int newValue = Value - 1;
                    if (newValue >= Min)
                        AssociatedObject.Text = newValue.ToString(CultureInfo.CurrentCulture);
                    e.Handled = true;
                    return;
                }
            }
        }
    }
}
