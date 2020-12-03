using CvGenerator.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CvGenerator
{
    public partial class CvGenerator : Form
    {
        public CvGenerator()
        {
            InitializeComponent();
            FillMockData();
        }
        private void FillMockData()
        {
            txtFirstName.Text = "Vahagn";
            txtLastName.Text = "Ghlijyan";
            txtAge.Text = "31";
         
            textBoxProfile.Text = ".Net developer";
            textBoxRole.Text = "Middle";
            textEmail.Text = "vghlijyan@gmail.com";
            textBoxPhone.Text = "094689621";

            textboxTitle1.Text = "junior developer";
            textBoxCompany.Text = "Mersoft";


            textboxTitle2.Text = "middel developer";
            textBoxCompany2.Text = "Mersoft";

            textboxTitle3.Text = "middel developer";
            textBoxCompany3.Text = "It Company";

            txtAdditional.Text = "Where can I get some? There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn't anything embarrassing hidden in the middle of text. ";
           
        }
        private Cv CreateCvModel()
        {

            return new Cv
            {

                PersonalInfo = new PersonalInfo
                {
                    FirstName = txtFirstName.Text,
                    LastName = txtFirstName.Text,
                    Age = Int32.Parse(txtAge.Text),
                    Profile = textBoxProfile.Text,
                    Phone = textBoxPhone.Text,
                    Role = textBoxRole.Text,
                    Email = textEmail.Text

                },
                Experience = new List<Experience>
                {
                    new Experience
                    {
                        Title=textboxTitle1.Text,
                        Company=textBoxCompany.Text,
                        From=dateTimePickerFrom1.Value,
                        To=dateTimePickerTo1.Value
                    },
                    new Experience
                    {
                         Title=textboxTitle2.Text,
                        Company=textBoxCompany2.Text,
                        From=dateTimePickerFrom2.Value,
                        To=dateTimePickerTo2.Value
                    },
                    new Experience
                    {
                         Title=textboxTitle3.Text,
                        Company=textBoxCompany3.Text,
                        From=dateTimePickerFrom3.Value,
                        To=dateTimePickerTo3.Value
                    },
                    //TODO for others 
                },
                AdditionalInfo = txtAdditional.Text
            };

             
        }
        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            MakePDFCv();
        }
        private void txtAge_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private bool CvValidation()


        {
            if (string.IsNullOrWhiteSpace(txtAge.Text))
            {
                txtAge.Focus();
                return false;

            }
            //TODO Validation Logic
            return true;
        }

        private void MakePDFCv()
        {
            if (!CvValidation())
                return;

            var Cvmanager = new CvGeneratorManager(new PdfCvGenerator());

            var cv = CreateCvModel();
            var isGenerated = Cvmanager.CreateCv(cv);
            if (isGenerated)
                MessageBox.Show("Your CV has been created successful", "CV information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Error occurred during creating CV", "CV information", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
