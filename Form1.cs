namespace todolist
{
    public partial class Form1 : Form
    {
        private const string PlaceholderAd = "Tapşırığın adı";
        private const string PlaceholderTesvir = "Təsvir";
        private bool isDeleting = false;

        public Form1()
        {
            InitializeComponent();
            InitializeCustomControls();
        }

        private void InitializeCustomControls()
        {
            button1.Text = "button1 — Əlavə et";

            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd.MM.yyyy HH:mm";

            SetPlaceholder(textBox1, PlaceholderAd);
            SetPlaceholder(textBox2, PlaceholderTesvir);

            textBox1.Enter += TextBox1_Enter;
            textBox1.Leave += TextBox1_Leave;

            textBox2.Enter += TextBox2_Enter;
            textBox2.Leave += TextBox2_Leave;

            button1.Click += button1_Click;
            checkedListBox1.ItemCheck += checkedListBox1_ItemCheck;
        }

        private void TextBox1_Enter(object sender, EventArgs e)
        {
            RemovePlaceholder(textBox1, PlaceholderAd);
        }

        private void TextBox1_Leave(object sender, EventArgs e)
        {
            SetPlaceholder(textBox1, PlaceholderAd);
        }

        private void TextBox2_Enter(object sender, EventArgs e)
        {
            RemovePlaceholder(textBox2, PlaceholderTesvir);
        }

        private void TextBox2_Leave(object sender, EventArgs e)
        {
            SetPlaceholder(textBox2, PlaceholderTesvir);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void SetPlaceholder(TextBox tb, string placeholder)
        {
            if (string.IsNullOrWhiteSpace(tb.Text) || (tb.Text == placeholder && tb.ForeColor == Color.Gray))
            {
                tb.Text = placeholder;
                tb.ForeColor = Color.Gray;
            }
        }

        private void RemovePlaceholder(TextBox tb, string placeholder)
        {
            if (tb.Text == placeholder && tb.ForeColor == Color.Gray)
            {
                tb.Text = "";
                tb.ForeColor = SystemColors.WindowText;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool adBosdur = string.IsNullOrWhiteSpace(textBox1.Text) || (textBox1.Text == PlaceholderAd && textBox1.ForeColor == Color.Gray);
            bool tesvirBosdur = string.IsNullOrWhiteSpace(textBox2.Text) || (textBox2.Text == PlaceholderTesvir && textBox2.ForeColor == Color.Gray);

            if (!adBosdur && !tesvirBosdur)
            {
                string ad = textBox1.Text.Trim();
                string tesvir = textBox2.Text.Trim();
                string tarix = dateTimePicker1.Value.ToString("dd.MM.yyyy HH:mm");

                checkedListBox1.Items.Add($"{ad} - {tesvir} ({tarix})", false);

                SetPlaceholder(textBox1, PlaceholderAd);
                SetPlaceholder(textBox2, PlaceholderTesvir);
            }
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (isDeleting)
            {
                return;
            }

            if (e.NewValue == CheckState.Checked)
            {
                DialogResult result = MessageBox.Show("Silmək istəyirsiniz?", "Silmə təsdiqi", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    int index = e.Index;
                    isDeleting = true;
                    checkedListBox1.BeginInvoke(new Action(() =>
                    {
                        try
                        {
                            if (index >= 0 && index < checkedListBox1.Items.Count)
                            {
                                checkedListBox1.Items.RemoveAt(index);
                            }
                        }
                        finally
                        {
                            isDeleting = false;
                        }
                    }));
                }
                else
                {
                    e.NewValue = CheckState.Unchecked;
                }
            }
        }
    }
}
