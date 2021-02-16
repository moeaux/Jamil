namespace aux_utils_hti_002
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        string script1 = @"
        ping 8.8.8.8";
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() => RunScript(script1));
            //RunScript(script1);
            //Task.Factory.StartNew(() => RunScript(script1));
            //Task.Run(() => txtConsole.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, new Action(() => RunScript(script1))));
        }

        
        public void RunScript(string script)
        {
            PowerShell psinstance = PowerShell.Create();
            psinstance.AddScript(script);
            Collection<PSObject> results = psinstance.Invoke();
            //StringBuilder stringBuilder = new StringBuilder();

            foreach (PSObject obj in results)
            {
                //stringBuilder.AppendLine(obj.ToString());
                txtConsole.Dispatcher.InvokeAsync((Action)delegate 
                {
                    txtConsole.Text += obj.ToString() + Environment.NewLine;
                    txtConsole.Refresh();
                });
            }
            txtConsole.Refresh();
        }

    }
}
