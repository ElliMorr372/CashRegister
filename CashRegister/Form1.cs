using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Linq.Expressions;
using System.Media;

namespace CashRegister
{
    public partial class Form1 : Form
    {
        double wandPrice = 175.40;
        double robePrice = 145.50;
        double bookPrice = 38.75;
        double wandNum = 0;
        double robeNum = 0;
        double bookNum = 0;
        double subtotal = 0;
        double taxRate = 0.13;
        double taxAmount = 0;
        double total = 0;
        double tendered = 0;
        double change = 0;
        public Form1()
        {
            InitializeComponent();

            //play store door chime when program opens
            SoundPlayer alertPlayer = new SoundPlayer(Properties.Resources.StoreDoorChime);
            alertPlayer.Play();

            //disble 2nd-4th buttons
            calculateChangeButton.Enabled = false;
            printReceiptButton.Enabled = false;
            newOrderButton.Enabled = false;
            receiptOutput.Size = new Size(0, 313);
        }

        private void calculateTotalsButton_Click(object sender, EventArgs e)
        {
            //play magical sound
            SoundPlayer alertPlayer = new SoundPlayer(Properties.Resources.MagicalSound);
            alertPlayer.Play();

            try
            {
                //get the inputs
                wandNum = Convert.ToDouble(wandNumInput.Text);
                robeNum = Convert.ToDouble(robeNumInput.Text);
                bookNum = Convert.ToDouble(bookNumInput.Text);

                //calculate the subtotal, the tax, and the total
                subtotal = wandNum * wandPrice + robeNum * robePrice + bookNum * bookPrice;
                taxAmount = subtotal * taxRate;
                total = subtotal + taxAmount;

                //output all values
                subtotalOutput.Text = $"{subtotal.ToString("C")}";
                taxAmountOutput.Text = $"{taxAmount.ToString("C")}";
                totalOutput.Text = $"{total.ToString("C")}";

                //enable next button
                calculateChangeButton.Enabled = true;
            }
            catch
            {
                wandNumInput.Text = "";
                robeNumInput.Text = "Input";
                bookNumInput.Text = "Error";
            }
        }

        private void calculateChangeButton_Click(object sender, EventArgs e)
        {
           //play magical sound
            SoundPlayer alertPlayer = new SoundPlayer(Properties.Resources.MagicalSound);
            alertPlayer.Play();

            //Check for insufficient funds
            if (tendered < total)
            {
                tenderedInput.Size = new Size(130, tenderedInput.Height);
                tenderedInput.Location = new Point(tenderedInput.Location.X - 50, tenderedInput.Location.Y);
                tenderedInput.Text = "Insufficient Funds";
            }
            else
            {
                //get the inputs
                tendered = Convert.ToDouble(tenderedInput.Text);

                //calculate the change
                change = tendered - total;

                //output the change
                changeOutput.Text = $"{change.ToString("C")}";

                //enable next button
                printReceiptButton.Enabled = true;

                //reset tendered input length
               tenderedInput.Size = new Size(43, tenderedInput.Height);
            }
        }

        private void printReceiptButton_Click(object sender, EventArgs e)
        {
            //play printing sound
            SoundPlayer alertPlayer = new SoundPlayer(Properties.Resources.PhotocopySound);
            alertPlayer.Play();

            //display all information on reciept using an array
            string[] ReceiptStrings = new string[13];
            ReceiptStrings[0] = $"      The Shop That Must";
            ReceiptStrings[1] = $"\n        Not Be Named";
            ReceiptStrings[2] = $"\n\n  Order Number: 3567";
            ReceiptStrings[3] = $"\n  September 29th, 2022";
            ReceiptStrings[4] = $"\n\n  Wands     x{wandNum} @ {wandPrice.ToString("C")}";
            ReceiptStrings[5] = $"\n  Robes     x{robeNum} @ {robePrice.ToString("C")}";
            ReceiptStrings[6] = $"\n  Books     x{bookNum} @ {bookPrice.ToString("C")}";
            ReceiptStrings[7] = $"\n\n  Subtotal     {subtotal.ToString("C")}";
            ReceiptStrings[8] = $"\n  Tax          {taxAmount.ToString("C")}";
            ReceiptStrings[9] = $"\n  Total        {total.ToString("C")}";
            ReceiptStrings[10] = $"\n\n  Tendered     {tendered.ToString("C")}";
            ReceiptStrings[11] = $"\n  Change       {change.ToString("C")}";
            ReceiptStrings[12] = $"\n\n      Have A Great Day!";

            for (int i = 0; i < ReceiptStrings.Length; i++)
            {
                receiptOutput.Text += ReceiptStrings[i];
                Refresh();
                Thread.Sleep(450);
            }

            //enable next button
            newOrderButton.Enabled = true;
        }

        private void newOrderButton_Click(object sender, EventArgs e)
        {
            //play magical sound
            SoundPlayer alertPlayer = new SoundPlayer(Properties.Resources.MagicalSound);
            alertPlayer.Play();

            //clear all outputs
            subtotalOutput.Text = $"";
            taxAmountOutput.Text = $"";
            totalOutput.Text = $"";
            changeOutput.Text = $"";
            receiptOutput.Text = $"";

            //clear all inputs
            wandNumInput.Text = $"";
            robeNumInput.Text = $"";
            bookNumInput.Text = $"";
            tenderedInput.Text = $"";

            //reset all variables
            wandNum = 0;
            robeNum = 0;
            bookNum = 0;
            subtotal = 0;
            taxAmount = 0;
            total = 0;
            tendered = 0;
            change = 0;

            //make change, recipet, and order button disabled
            calculateChangeButton.Enabled = false;
            printReceiptButton.Enabled = false;
            newOrderButton.Enabled = false;
        }
    }
}
