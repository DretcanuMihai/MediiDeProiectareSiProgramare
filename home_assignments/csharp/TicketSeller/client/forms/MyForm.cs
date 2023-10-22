using System.Windows.Forms;
using services.interfaces;

namespace client.forms;

public partial class MyForm : Form
{
    protected MainController _controller;

    public MyForm(MainController controller)
    {
        _controller = controller;
        InitializeComponent();
    }
}