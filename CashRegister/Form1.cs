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
using System.Drawing.Drawing2D;

namespace CashRegister
{
    public partial class Form1 : Form
    {
        //set variables for prices, number of items, tax, subtotal, total, tendered, and change
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

        //create booleans for the reciept and hidden message
        Boolean receiptPrinted = false;
        Boolean messageShown = false;
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
            hiddenMessageOutput.Visible = false;

            //set receipt to an invisible height
            receiptOutput.Size = new Size(242, 0);
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

                //enable change button
                calculateChangeButton.Enabled = true;
            }
            catch
            {
                //display input error
                wandNumInput.Text = "Input";
                robeNumInput.Text = "Error";
                bookNumInput.Text = "  !!!!!";
            }
        }
        private void calculateChangeButton_Click(object sender, EventArgs e)
        {
            //play magical sound
            SoundPlayer alertPlayer = new SoundPlayer(Properties.Resources.MagicalSound);
            alertPlayer.Play();

            //get the inputs
            tendered = Convert.ToDouble(tenderedInput.Text);

            //Check for insufficient funds
            if (tendered < total)
            {
                tenderedInput.Size = new Size(130, tenderedInput.Height);
                tenderedInput.Location = new Point(tenderedInput.Location.X - 50, tenderedInput.Location.Y);
                tenderedInput.Text = "Insufficient Funds";
            }
            else
            {
                //reset tendered input length
                tenderedInput.Size = new Size(43, tenderedInput.Height);
                tenderedInput.Location = new Point(181, 356);

                //calculate the change
                change = tendered - total;

                //output the change
                changeOutput.Text = $"{change.ToString("C")}";

                //enable receipt button
                printReceiptButton.Enabled = true;
            }
        }

        private void printReceiptButton_Click(object sender, EventArgs e)
        {
            if (receiptPrinted == false)
            {

                //disable print receipt button
                printReceiptButton.Enabled = false;

                //play printing sound
                SoundPlayer alertPlayer = new SoundPlayer(Properties.Resources.PhotocopySound);
                alertPlayer.Play();

                //display reciept using an array
                string[] ReceiptStrings = new string[13];
                ReceiptStrings[0] = $"      The Shop That Must";
                ReceiptStrings[1] = $"\n        Not Be Named";
                ReceiptStrings[2] = $"\n\n  Order Number: 717";
                ReceiptStrings[3] = $"\n  October 3rd, 2022";
                ReceiptStrings[4] = $"\n\n  Wands     x{wandNum} @ {wandPrice.ToString("C")}";
                ReceiptStrings[5] = $"\n  Robes     x{robeNum} @ {robePrice.ToString("C")}";
                ReceiptStrings[6] = $"\n  Books     x{bookNum} @ {bookPrice.ToString("C")}";
                ReceiptStrings[7] = $"\n\n  Subtotal       {subtotal.ToString("C")}";
                ReceiptStrings[8] = $"\n  Tax            {taxAmount.ToString("C")}";
                ReceiptStrings[9] = $"\n  Total          {total.ToString("C")}";
                ReceiptStrings[10] = $"\n\n  Tendered       {tendered.ToString("C")}";
                ReceiptStrings[11] = $"\n  Change         {change.ToString("C")}";
                ReceiptStrings[12] = $"\n\n      Have A Great Day!";

                for (int i = 0; i < ReceiptStrings.Length; i++)
                {
                    receiptOutput.Height = receiptOutput.Height + 25;
                    receiptOutput.Size = new Size(receiptOutput.Width, receiptOutput.Height);
                    receiptOutput.Text += ReceiptStrings[i];
                    Refresh();
                    Thread.Sleep(450);
                }

                //enable new order button
                newOrderButton.Enabled = true;

                //set receipt boolean to true
                receiptPrinted = true;
            }
            else
            {
            }
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

            //reset receipt position
            receiptOutput.Size = new Size(242, 0);

            //clear all inputs
            wandNumInput.Text = $"0";
            robeNumInput.Text = $"0";
            bookNumInput.Text = $"0";
            tenderedInput.Text = $"0";

            //reset all variables
            wandNum = 0;
            robeNum = 0;
            bookNum = 0;
            subtotal = 0;
            taxAmount = 0;
            total = 0;
            tendered = 0;
            change = 0;

            //disable change, receipt, and new order button
            calculateChangeButton.Enabled = false;
            printReceiptButton.Enabled = false;
            newOrderButton.Enabled = false;

            //reset booleans to false
            messageShown = false;
            receiptPrinted = false;
        }

        private void gryffindorPicture2_Click(object sender, EventArgs e)
        {
            if (messageShown == false)
            {
                //play magical sound
                SoundPlayer alertPlayer = new SoundPlayer(Properties.Resources.PictureButtonPress);
                alertPlayer.Play();

                //make hidden message label visible
                hiddenMessageOutput.Visible = true;

                //display hidden message with an array
                string[] HiddenMessageStrings = new string[11];
                HiddenMessageStrings[0] = $"\nWelcome";
                HiddenMessageStrings[1] = $"\nand";
                HiddenMessageStrings[2] = $"\nCongratulations!";
                HiddenMessageStrings[3] = $"\n\nYou found the secret";
                HiddenMessageStrings[4] = $"\nmessage only for";
                HiddenMessageStrings[5] = $"\nwitches and wizards.";
                HiddenMessageStrings[6] = $"\n\nNow...";
                HiddenMessageStrings[7] = $"\ngo forth on your";
                HiddenMessageStrings[8] = $"\nmagical journey!!";
                HiddenMessageStrings[9] = $"\n";
                HiddenMessageStrings[10] = $"\n";

                //use for loop to print hidden message one line at a time
                for (int i = 0; i < HiddenMessageStrings.Length; i++)
                {
                    hiddenMessageOutput.Text += HiddenMessageStrings[i];
                    Refresh();
                    Thread.Sleep(400);
                }

                //change border style when message array is finished
                hiddenMessageOutput.BorderStyle = BorderStyle.Fixed3D;

                //allow user to read for 6 seconds
                Refresh();
                Thread.Sleep(6000);

                //clear, reset, and disable hidden message
                hiddenMessageOutput.Visible = false;
                hiddenMessageOutput.Text = $"";
                messageShown = true;
            }
            else
            {
            }
        }
    }
}
