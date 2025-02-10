using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DentakuPractice
{
    public partial class fmLog : Form
    {

        private FmDentaku fmDentaku = null;

        private string rirekiAnswer = string.Empty;

        public string RirekiAnswer
        {
            get
            {
                return rirekiAnswer;
            }
            set
            {
                rirekiAnswer = value;
            }
        }


        public fmLog(FmDentaku fd)
        {
            InitializeComponent();

            fmDentaku = fd;
        }

        private int cntEnter = 0;
        private int cntButton = -1;
        private List<Button> buttons = new List<Button>();


        public void ShowRireki(IWin32Window owner, string agShiki, string agKotae)
        {

            cntEnter += 40;
            cntButton++;
            int wkTop = cntButton == 0 ? 20 : buttons[cntButton - 1].Top;

            Button btn = new Button();
            btn.Left = 20;
            btn.Top = wkTop;
            btn.Width = 180;
            btn.Height = 40;
            btn.Text = agShiki+ "\n" + agKotae;
            btn.TextAlign = ContentAlignment.MiddleRight;
            btn.Click += new EventHandler(TradeAnswer);
            panel1.Controls.Add(btn);
            foreach (var button in buttons)
            {
                button.Top += 40;
            }
            buttons.Add(btn);
        }

        private void fmLog_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;
        }

        public void TradeAnswer(object sender, EventArgs e)
        {
            Button btnString = (Button)sender;
           
            this.rirekiAnswer = btnString.Text.Substring(btnString.Text.LastIndexOf("＝") + 2);

            fmDentaku.btnRireki_Click();
        }
    }
}
