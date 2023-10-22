using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using model.entities;

namespace client.forms;

public partial class MainForm : MyForm
{
    public ICollection<Festival> Festivals1 = new List<Festival>();
    public ICollection<Festival> Festivals2 = new List<Festival>();

    public MainForm(MainController controller) : base(controller)
    {
        InitializeComponent();
        _controller.updateEvent += UpdateTicketSold;
    }

    public void LoadGrid1()
    {
        Festivals1 = _controller.getFestivals();
        dataGridView1.DataSource = Festivals1;
        dataGridView1.Columns["ID"].Visible = false;
        dataGridView1.Columns["AvailableSpots"].Visible = false;
        int k = 0;
        foreach (DataGridViewRow row in dataGridView1.Rows)
        {
            if (Festivals1.ElementAt(k).RemainingSpots.Equals(0))
            {
                row.DefaultCellStyle.BackColor = Color.Red;
            }

            k++;
        }
    }

    public void LoadGrid2()
    {
        try
        {
            DateTime dateTime = dateTimePicker.Value;
            Festivals2 = _controller.getFestivalsOnDate(dateTime);
            dataGridView2.DataSource = Festivals2;
            dataGridView2.Columns["ID"].Visible = false;
            dataGridView2.Columns["AvailableSpots"].Visible = false;
            int k = 0;
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (Festivals2.ElementAt(k).RemainingSpots.Equals(0))
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                }

                k++;
            }
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message);
        }
    }

    public void updateGrid1(Ticket ticket)
    {
        int k = 0;

        foreach (Festival festival in Festivals1)
        {
            if (festival.Id.Equals(ticket.Festival.Id))
            {
                festival.SoldSpots = festival.SoldSpots + ticket.NumberOfSpots;
                break;
            }

            k++;
        }

        if (Festivals1.ElementAt(k).RemainingSpots.Equals(0))
        {
            dataGridView1.Rows[k].DefaultCellStyle.BackColor = Color.Red;
        }

        dataGridView1.Refresh();
    }

    public void updateGrid2(Ticket ticket)
    {
        int k = 0;
        bool ok = false;

        foreach (Festival festival in Festivals2)
        {
            if (festival.Id.Equals(ticket.Festival.Id))
            {
                festival.SoldSpots = festival.SoldSpots + ticket.NumberOfSpots;
                ok = true;
                break;
            }

            k++;
        }

        if (ok && Festivals2.ElementAt(k).RemainingSpots.Equals(0))
        {
            dataGridView2.Rows[k].DefaultCellStyle.BackColor = Color.Red;
        }

        dataGridView2.Refresh();
    }

    private void refreshButton_Click(object sender, EventArgs e)
    {
        LoadGrid1();
    }

    private void logoutButton_Click(object sender, EventArgs e)
    {
        try
        {
            _controller.logout();
            _controller.updateEvent -= UpdateTicketSold;
            LoginForm loginForm = new LoginForm(_controller);
            loginForm.Show();
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void searchButton_Click(object sender, EventArgs e)
    {
        LoadGrid2();
    }

    private void MainForm_Activated(object sender, EventArgs e)
    {
        LoadGrid1();
        allRadioButton.Checked = true;
    }

    private void buyButton_Click(object sender, EventArgs e)
    {
        try
        {
            int id = 0;
            if (allRadioButton.Checked)
            {
                if (dataGridView1.SelectedCells.Count == 0)
                    throw new Exception("No item selected!");
                id = Festivals1.ElementAt(dataGridView1.SelectedCells[0].RowIndex).Id;
            }
            else
            {
                if (dataGridView2.SelectedCells.Count == 0)
                    throw new Exception("No item selected!");
                id = Festivals2.ElementAt(dataGridView2.SelectedCells[0].RowIndex).Id;
            }

            _controller.buyTicket(id, buyerNameTextBox.Text, Int32.Parse(spotsTextBox.Text));
            MessageBox.Show("Ticket succesfully bought!");
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message);
        }
    }

    public delegate void UpdateListBoxCallback(Ticket ticket);

    public void UpdateTicketSold(object sender, TicketEventArgs args)
    {
        if (args.UserEventType == TicketEvent.TicketBought)
        {
            Ticket ticket = (Ticket) args.Data;
            dataGridView1.BeginInvoke(new UpdateListBoxCallback(this.updateGrid1), ticket);
            dataGridView2.BeginInvoke(new UpdateListBoxCallback(this.updateGrid2), ticket);
        }
    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (e.CloseReason == CloseReason.UserClosing)
        {
            try
            {
                _controller.logout();
                _controller.updateEvent -= UpdateTicketSold;
                Application.Exit();
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }
        }
    }
}