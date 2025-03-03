using System;
using System.Threading.Tasks;
using System.Windows;
using System.ServiceModel;
using System.Threading;

namespace SIT_WPF
{
    public partial class MainWindow : Window
    {
        private CancellationTokenSource cancellationTokenSource;
        private ServiceHost host;
        private IRequestService client;
        public MainWindow()
        {
            InitializeComponent();
            StartServiceHost();
            InitializeServiceClient();
            StartPeriodicCalls();
        }

        private void StartServiceHost()
        {
            Task.Run(() =>
            {
                try
                {
                    host = new ServiceHost(typeof(RequestService));
                    host.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to start service: {ex.Message}");
                }
            });
        }

        private void InitializeServiceClient()
        {
            try
            {
                var factory = new ChannelFactory<IRequestService>(
                    new BasicHttpBinding(),
                    new EndpointAddress("http://localhost:8733/RequestService/")
                );

                client = factory.CreateChannel();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to connect to WCF Service: {ex.Message}");
            }
        }

        private void StartPeriodicCalls()
        {
            cancellationTokenSource = new CancellationTokenSource();
            CancellationToken token = cancellationTokenSource.Token;

            Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    try
                    {
                        await Task.Delay(10000); 
                        CheckRequests();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error during periodic call: " + ex.Message);
                    }
                }
            }, token);
        }

        private void CheckRequests()
        {
            var requests = client.CheckForRequests();
            Dispatcher.Invoke(() =>
            {
                RequestGrid.ItemsSource = requests;
            });
        }

        private void ApproveRequest_Click(object sender, RoutedEventArgs e)
        {
            if (RequestGrid.SelectedItem is Request selectedRequest)
            {
                try
                {
                    client.UpdateRequest(selectedRequest.RequestID, true);
                    MessageBox.Show($"Request {selectedRequest.RequestID} Approved!");
                    CheckRequests(); // Refresh the grid
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error approving request: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please select a request to approve.");
            }
        }

        private void RejectRequest_Click(object sender, RoutedEventArgs e)
        {
            if (RequestGrid.SelectedItem is Request selectedRequest)
            {
                try
                {
                    client.UpdateRequest(selectedRequest.RequestID, false);
                    MessageBox.Show($"Request {selectedRequest.RequestID} Rejected!");
                    CheckRequests(); // Refresh the grid
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error rejecting request: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please select a request to reject.");
            }
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            if (host != null && host.State == CommunicationState.Opened)
            {
                host.Close();
                Console.WriteLine("Service stopped.");
            }

            // Cancel the periodic calls
            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
                Console.WriteLine("Periodic calls stopped.");
            }
        }
    }

}
