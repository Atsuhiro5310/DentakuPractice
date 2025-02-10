
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace DentakuPractice
{
    public partial class FmDentaku : Form
    {

        private fmLog _fmLog = null;

        public FmDentaku()
        {
            InitializeComponent();
            _fmLog = new fmLog(this);
        }

        /// <summary>
        /// 丸みを帯びたツール生成コード
        /// </summary>
        /// <param name="getObject"></param>
        /// <param name="getRadius"></param>
        private void StartCircle(dynamic getObject, int getRadius)
        {

            //***   どの程度丸めるかを決める   ***
            int diameter = getRadius * 2;

            //***   描画クラスを生成   ***
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();

            // 左上
            gp.AddPie(0, 0, diameter, diameter, 180, 90);

            // 右上
            gp.AddPie(getObject.Width - diameter, 0, diameter, diameter, 270, 90);

            // 左下
            gp.AddPie(0, getObject.Height - diameter, diameter, diameter, 90, 90);

            // 右下
            gp.AddPie(getObject.Width - diameter, getObject.Height - diameter, diameter, diameter, 0, 90);

            // 中央
            gp.AddRectangle(new Rectangle(getRadius, 0, getObject.Width - diameter, getObject.Height));

            // 左
            gp.AddRectangle(new Rectangle(0, getRadius, getRadius, getObject.Height - diameter));

            // 右
            gp.AddRectangle(new Rectangle(getObject.Width - getRadius, getRadius, getRadius, getObject.Height - diameter));
            getObject.Region = new Region(gp);
        }

        /// <summary>
        /// 丸みを帯びたツールを生成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FmDentaku_Load(object sender, EventArgs e)
        {

            //***   ボタン、値表示部、退避項表示部、フォームの角を丸める   ***
            StartCircle(btn0, 10);
            StartCircle(btn00, 10);
            StartCircle(btn1, 10);
            StartCircle(btn2, 10);
            StartCircle(btn3, 10);
            StartCircle(btn4, 10);
            StartCircle(btn5, 10);
            StartCircle(btn6, 10);
            StartCircle(btn7, 10);
            StartCircle(btn8, 10);
            StartCircle(btn9, 10);
            StartCircle(btnClear, 10);
            StartCircle(btnDelete, 10);
            StartCircle(btnConvert, 10);
            StartCircle(btnDiv, 10);
            StartCircle(btnMulti, 10);
            StartCircle(btnMinus, 10);
            StartCircle(btnPlus, 10);
            StartCircle(btnPoint, 10);
            StartCircle(btnEqual, 10);
            StartCircle(panel1, 10);
            StartCircle(this, 10);
        }

        //***   マウスのクリック位置を記憶   ***
        private Point mousePoint;

        ///*-------------------------------------------------------------------*
        /// <summary>
        /// クリック時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///*-------------------------------------------------------------------*
        private void FmDentaku_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {

                //***   位置を記憶する   ***
                mousePoint = new Point(e.X, e.Y);
            }
        }

        ///*-------------------------------------------------------------------*
        /// <summary>
        /// ドラッグ時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///*-------------------------------------------------------------------*
        private void FmDentaku_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {

                //***   動かすたび位置を変更する   ***
                this.Left += e.X - mousePoint.X;
                this.Top += e.Y - mousePoint.Y;
            }
        }

        ///*-------------------------------------------------------------------*
        /// <summary>
        /// フォーカスを解除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///*-------------------------------------------------------------------*
        private void btnNotEnter(object sender, EventArgs e)
        {

            //***   フォーカスを解除   ***
            ActiveControl = null;
        }

        private string numSahen = "";
        private string numUhen = "";
        private string wkEnzanshi = "";
        private string numAns = "";
        private int cntButton = 0;

        private List<List<Class1>> rireki = new List<List<Class1>>();

        ///*-------------------------------------------------------------------*
        /// <summary>
        /// ボタンをクリックしたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///*-------------------------------------------------------------------*
        private void btnCommon_Click(object sender, EventArgs e)
        {

            //***   押されたボタンの情報を取得   ***
            Button btnCommon = (Button)sender;

            //***   パターンを数字にする   ***
            Regex regex = new Regex(@"[^0-9]");

            if (btnCommon.Text == "0" || btnCommon.Text == "00" || btnCommon.Text == "1" || btnCommon.Text == "2" || btnCommon.Text == "3" || btnCommon.Text == "4" || btnCommon.Text == "5" || btnCommon.Text == "6" || btnCommon.Text == "7" || btnCommon.Text == "8" || btnCommon.Text == "9")
            {

                cntButton++;

                if (lblEnzanshi.Text != "")
                {
                    txtDisplay.Text = "0";
                    lblEnzanshi.Text = "";
                }

                //***   数字のみを取得   ***
                string numLength = regex.Replace(txtDisplay.Text, "");

                //***   入力している値に小数点が含まれているかを判定   ***
                bool boolPoint = txtDisplay.Text.Contains(".");

                if (numLength.Length == 8 && btnCommon.Text == "00")
                {
                    txtDisplay.Text += "0";
                    return;
                }

                if (numLength.Length <= 8)
                {

                    if (boolPoint)
                    {
                        txtDisplay.Text += btnCommon.Text;
                        if (!(btnCommon.Text == "0" || btnCommon.Text == "00"))
                        {
                            txtDisplay.Text = string.Format("{0:#,0.########}", decimal.Parse(txtDisplay.Text));
                        }
                    }
                    else if (txtDisplay.Text == "0")
                    {
                        txtDisplay.Text = btnCommon.Text;
                        txtDisplay.Text = string.Format("{0:#,0}", decimal.Parse(txtDisplay.Text));
                    }
                    else
                    {
                        txtDisplay.Text += btnCommon.Text;
                        txtDisplay.Text = string.Format("{0:#,0}", decimal.Parse(txtDisplay.Text));
                    }

                }

                return;
            }

            if (btnCommon.Text == "C")
            {
                numSahen = string.Empty;
                numUhen = string.Empty;
                wkEnzanshi = string.Empty;
                numAns = string.Empty;
                txtDisplay.Text = "0";
                lblEnzanshi.Text = "";
                cntButton = 0;
                return;
            }

            if (btnCommon.Text == ".")
            {
                if (txtDisplay.Text.IndexOf(".") < 0)
                {
                    txtDisplay.Text += ".";
                }
                return;
            }

            if (btnCommon.Text == "+/-")
            {

                if (cntButton == 0 && lblEnzanshi.Text == "＝") { }
                else if (cntButton == 0) return;


                if (txtDisplay.Text != "0")
                {
                    if (txtDisplay.Text.IndexOf("-") == 0)
                    {
                        txtDisplay.Text = txtDisplay.Text.Substring(1);
                    }
                    else
                    {
                        txtDisplay.Text = "-" + txtDisplay.Text;
                    }
                }
                return;
            }

            if (btnCommon.Text == "←")
            {
                if (lblEnzanshi.Text != "")
                {
                    return;
                }

                if (txtDisplay.Text == "0")
                {
                    cntButton = 0;
                    return;
                }
                else if (txtDisplay.Text.Length == 1 || txtDisplay.Text.IndexOf("-") == 0 && txtDisplay.Text.Length == 2)
                {
                    txtDisplay.Text = "0";
                }
                else
                {
                    txtDisplay.Text = txtDisplay.Text.Remove(txtDisplay.Text.Length - 1);
                    if (!txtDisplay.Text.Contains("."))
                    {
                        txtDisplay.Text = string.Format("{0:#,0.########}", decimal.Parse(txtDisplay.Text));
                    }
                }
                return;
            }

            if (btnCommon.Text == "＋" || btnCommon.Text == "－" || btnCommon.Text == "×" || btnCommon.Text == "÷")
            {

                if (cntButton == 0 && lblEnzanshi.Text == "＝")
                {
                    numSahen = txtDisplay.Text;
                    numAns = "";
                    lblEnzanshi.Text = btnCommon.Text;
                    wkEnzanshi = btnCommon.Text;
                    return;
                }

                if (numSahen == "" && cntButton == 0) return;

                if (cntButton == 0)
                {
                    lblEnzanshi.Text = btnCommon.Text;
                    wkEnzanshi = btnCommon.Text;
                    return;
                }

                string wkSahen = numSahen;

                switch (wkEnzanshi)
                {
                    case "＋":
                        numSahen = (decimal.Parse(numSahen) + decimal.Parse(txtDisplay.Text)).ToString();
                        numSahen = KetasuShitei(numSahen);
                        txtDisplay.Text = numSahen;
                        break;
                    case "－":
                        numSahen = (decimal.Parse(numSahen) - decimal.Parse(txtDisplay.Text)).ToString();
                        numSahen = KetasuShitei(numSahen);
                        txtDisplay.Text = numSahen;
                        break;
                    case "×":
                        numSahen = (decimal.Parse(numSahen) * decimal.Parse(txtDisplay.Text)).ToString();
                        numSahen = KetasuShitei(numSahen);
                        txtDisplay.Text = numSahen;
                        break;
                    case "÷":
                        if (txtDisplay.Text == "0")
                        {
                            txtDisplay.Text = "(/・ω・)/";
                            MessageBox.Show("0では割れません");
                            txtDisplay.Text = wkSahen;
                            numSahen = wkSahen;
                            lblEnzanshi.Text = wkEnzanshi;
                            cntButton = 0;
                            return;
                        }
                        numSahen = (decimal.Parse(numSahen) / decimal.Parse(txtDisplay.Text)).ToString();
                        numSahen = KetasuShitei(numSahen);
                        txtDisplay.Text = numSahen;
                        break;
                }

                if (decimal.Parse(txtDisplay.Text) > 999999999 || decimal.Parse(txtDisplay.Text) < -999999999)
                {
                    txtDisplay.Text = "( ﾟДﾟ)";
                    MessageBox.Show("ERROR");
                    txtDisplay.Text = wkSahen;
                    numSahen = wkSahen;
                    lblEnzanshi.Text = wkEnzanshi;
                    cntButton = 0;
                    return;
                }

                lblEnzanshi.Text = btnCommon.Text;
                numSahen = txtDisplay.Text;
                wkEnzanshi = btnCommon.Text;
                cntButton = 0;
            }

            if (btnCommon.Text == "＝")
            {

                if (cntButton == 0)
                {
                    return;
                }

                if (wkEnzanshi == string.Empty)
                {
                    return;
                }

                lblEnzanshi.Text = btnCommon.Text;
                numUhen = txtDisplay.Text;

                switch (wkEnzanshi)
                {
                    case "＋":
                        numAns = (decimal.Parse(numSahen) + decimal.Parse(numUhen)).ToString();
                        numAns = KetasuShitei(numAns);
                        txtDisplay.Text = numAns;
                        break;
                    case "－":
                        numAns = (decimal.Parse(numSahen) - decimal.Parse(numUhen)).ToString();
                        numAns = KetasuShitei(numAns);
                        txtDisplay.Text = numAns;
                        break;
                    case "×":
                        numAns = (decimal.Parse(numSahen) * decimal.Parse(numUhen)).ToString();
                        numAns = KetasuShitei(numAns);
                        txtDisplay.Text = numAns;
                        break;
                    case "÷":
                        if (numUhen == "0")
                        {
                            txtDisplay.Text = "(/・ω・)/";
                            MessageBox.Show("0では割れません");
                            txtDisplay.Text = numSahen;
                            lblEnzanshi.Text = wkEnzanshi;
                            numUhen = string.Empty;
                            numAns = string.Empty;
                            cntButton = 0;
                            return;
                        }
                        numAns = (decimal.Parse(numSahen) / decimal.Parse(numUhen)).ToString();
                        numAns = KetasuShitei(numAns);
                        txtDisplay.Text = numAns;
                        break;
                }

                if (decimal.Parse(txtDisplay.Text) > 999999999 || decimal.Parse(txtDisplay.Text) < -999999999)
                {

                    txtDisplay.Text = "(´・ω・｀)";
                    MessageBox.Show("ERROR");
                    txtDisplay.Text = numSahen;
                    lblEnzanshi.Text = wkEnzanshi;
                    numUhen = string.Empty;
                    numAns = string.Empty;
                    cntButton = 0;
                    return;
                }

                if (numSahen.Contains("-"))
                {
                    numSahen = "(" + numSahen + ")";
                }
                if (numUhen.Contains("-"))
                {
                    numUhen = "(" + numUhen + ")";
                }

                string wkShiki = numSahen + " " + wkEnzanshi + " " + numUhen + " ＝";
                string wkKotae = numAns;

                _fmLog.ShowRireki(this, wkShiki, wkKotae);
                numUhen = "";
                numAns = string.Empty;
                wkEnzanshi = "";
                cntButton = 0;
            }
        }

        private void FmDentaku_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }

            if (e.KeyCode == Keys.H)
            {
                MessageBox.Show("[" + numSahen + "][" + numUhen + "][" + wkEnzanshi + "][" + numAns + "]");
            }
        }

        private void btnLog_Click(object sender, EventArgs e)
        {

            _fmLog.Left = this.Left + 235;
            _fmLog.Top = this.Top + 50;
            _fmLog.StartPosition = FormStartPosition.Manual;

            if (_fmLog.Visible == true)
            {
                _fmLog.Show();
            }
            else
            {
                _fmLog.Visible = true;
            }
        }

        public void btnRireki_Click()
        {
            txtDisplay.Text = _fmLog.RirekiAnswer;
            lblEnzanshi.Text = string.Empty;
            cntButton++;
        }

        private string KetasuShitei(string str)
        {

            //***   9桁にした値の退避領域   ***
            string numResult = "";

            //***   引数を数値に変換   ***
            decimal numBefore = decimal.Parse(str);

            //***   小数点より左が何桁かを調べる   ***
            string[] numPointLeft = str.Split(new[] { "." }, StringSplitOptions.None);

            //***   合計9桁になるようにStringFormatする   ***
            if (str.Contains("-"))
            {
                if (numPointLeft[0].Length == 2)
                {
                    numResult = String.Format("{0:#,0.########}", numBefore);
                }
                else if (numPointLeft[0].Length == 3)
                {
                    numResult = String.Format("{0:#,0.#######}", numBefore);
                }
                else if (numPointLeft[0].Length == 4)
                {
                    numResult = String.Format("{0:#,0.######}", numBefore);
                }
                else if (numPointLeft[0].Length == 5)
                {
                    numResult = String.Format("{0:#,0.#####}", numBefore);
                }
                else if (numPointLeft[0].Length == 6)
                {
                    numResult = String.Format("{0:#,0.####}", numBefore);
                }
                else if (numPointLeft[0].Length == 7)
                {
                    numResult = String.Format("{0:#,0.###}", numBefore);
                }
                else if (numPointLeft[0].Length == 8)
                {
                    numResult = String.Format("{0:#,0.##}", numBefore);
                }
                else if (numPointLeft[0].Length == 9)
                {
                    numResult = String.Format("{0:#,0.#}", numBefore);
                }
                else if (numPointLeft[0].Length >= 10
                    )
                {
                    numResult = String.Format("{0:#,0}", numBefore);
                }
            }
            else
            {
                if (numPointLeft[0].Length == 1)
                {
                    numResult = String.Format("{0:#,0.########}", numBefore);
                }
                else if (numPointLeft[0].Length == 2)
                {
                    numResult = String.Format("{0:#,0.#######}", numBefore);
                }
                else if (numPointLeft[0].Length == 3)
                {
                    numResult = String.Format("{0:#,0.######}", numBefore);
                }
                else if (numPointLeft[0].Length == 4)
                {
                    numResult = String.Format("{0:#,0.#####}", numBefore);
                }
                else if (numPointLeft[0].Length == 5)
                {
                    numResult = String.Format("{0:#,0.####}", numBefore);
                }
                else if (numPointLeft[0].Length == 6)
                {
                    numResult = String.Format("{0:#,0.###}", numBefore);
                }
                else if (numPointLeft[0].Length == 7)
                {
                    numResult = String.Format("{0:#,0.##}", numBefore);
                }
                else if (numPointLeft[0].Length == 8)
                {
                    numResult = String.Format("{0:#,0.#}", numBefore);
                }
                else if (numPointLeft[0].Length >= 9)
                {
                    numResult = String.Format("{0:#,0}", numBefore);
                }
            }
            

            return numResult;

        }
    }
}
