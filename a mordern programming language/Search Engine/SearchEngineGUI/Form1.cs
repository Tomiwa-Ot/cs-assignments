using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace SearchEngineGUI
{
    public partial class Form1 : Form
    {
        private ResultsForm ResultForm;
        private string textboxPlaceholder = "Search...";
        private List<string> Suggestions;
        private AutoCompleteStringCollection AutoCompleteSource;
        private string SuggestionsFile = @"C:\Users\ENVY 15\source\repos\SearchEngineGUI\suggestions.json";

        public Form1()
        {
            InitializeComponent();

            // Load autocomplete suggestions
            AutoCompleteSource = new AutoCompleteStringCollection();
            if (File.Exists(SuggestionsFile))
            {
                string json = File.ReadAllText(SuggestionsFile);
                Suggestions = JsonSerializer.Deserialize<List<string>>(json);
                AutoCompleteSource.AddRange(Suggestions.ToArray());
                SearchTextbox.AutoCompleteCustomSource = AutoCompleteSource;
                SearchTextbox.AutoCompleteMode = AutoCompleteMode.Suggest;
                SearchTextbox.AutoCompleteCustomSource = AutoCompleteSource;
            }
        }

        private void SearchTextbox_Enter(object sender, EventArgs e)
        {
            if (SearchTextbox.Text == textboxPlaceholder)
            {
                SearchTextbox.Text = "";
                SearchTextbox.ForeColor = Color.Black;
            }
        }

        private void SearchTextbox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SearchTextbox.Text))
            {
                SearchTextbox.Text = textboxPlaceholder;
                SearchTextbox.ForeColor = Color.Gray;
            }
        }

        private void SearchButtonPicture_Click(object sender, EventArgs e)
        {
            PerformSearch();   
        }

        /// <summary>
        /// Send user query to the search engine API
        /// </summary>
        private async void PerformSearch()
        {
            // Display error if query is empty
            if (string.IsNullOrEmpty(SearchTextbox.Text) || SearchTextbox.Text.Equals(textboxPlaceholder))
            {
                MessageBox.Show("Query cannot be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Add query to autocomplete suggestions if not part
                if (!Suggestions.Contains(SearchTextbox.Text))
                {
                    Suggestions.Add(SearchTextbox.Text);
                    string json = JsonSerializer.Serialize(Suggestions);
                    File.WriteAllText(SuggestionsFile, json);
                }

                // Get response from API
                var response = await Http.RequestAsync(Endpoint.SEARCH, new Dictionary<string, string>() { { "query", SearchTextbox.Text } });
                dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(response);

                // Open response in new form
                ResultForm = new ResultsForm(data.results.ToObject<List<string>>(), data.time.ToString());
                ResultForm.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SearchTextbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                PerformSearch();
        }
    }
}
