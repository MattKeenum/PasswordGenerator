﻿using PasswordGenerator.Core;
using PasswordGenerator.Models;
using System;
using System.Windows.Forms;

namespace PasswordGenerator.App
{
    public partial class MainApp : Form
    {
        private IPasswordGeneratorManager _passwordGenerator;

        public MainApp()
        {
            InitializeComponent();

            _passwordGenerator = new PasswordGeneratorManager();
            btnCopy.Enabled = false;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            Generate();
        }

        private void Generate()
        {
            try
            {
                if (ValidateUserInput())
                {
                    var passwordOptions = FillPasswordOptions();

                    var password = _passwordGenerator.Generate(passwordOptions);

                    txtPassword.Text = password.ToString();
                    btnCopy.Enabled = true;
                }                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error generating password", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }    
        }

        private bool ValidateUserInput()
        {
            var output = 0;
            if (String.IsNullOrEmpty(txtMaxSize.Text) || !int.TryParse(txtMaxSize.Text, out output) || Convert.ToInt32(txtMaxSize.Text) <= 0)
                throw new Exception("Invalid number input.\nMax size must be higher than zero.");

            if (!cbNumber.Checked && !cbSpecialCharacters.Checked && !cbUppercase.Checked && !cbLowercase.Checked)
                throw new Exception("Select at least one option");

            return true;
        }

        private PasswordOptions FillPasswordOptions()
        {
            var passwordOptions = new PasswordOptions();

            passwordOptions.MaxSize = Convert.ToInt32(txtMaxSize.Text);
            passwordOptions.Numbers = cbNumber.Checked;
            passwordOptions.SpecialCharacters = cbSpecialCharacters.Checked;
            passwordOptions.UpperCaseLetters = cbUppercase.Checked;
            passwordOptions.LowerCaseLetters = cbLowercase.Checked;

            return passwordOptions;
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtPassword.Text);
        }
    }
}
