using NetworkService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NetworkService.ViewModel
{
    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel()
        {
            createListener(); //Povezivanje sa serverskom aplikacijom
            CurrentViewModel = networkEntitiesViewModel;
            UndoDestinations.Add("network entities");
            NavCommand = new MyICommand<string>(OnNav, OnUndoNav);
            HomeCommand = new MyICommand(OnHome);
            ClosingMainWindow = new MyICommand(OnClosingMainWindow);
            UndoCommand = new MyICommand(OnUndo);
        }

        #region connection
                                // ######### ZAMENITI stvarnim brojem elemenata
                                //           zavisno od broja entiteta u listi
        private void createListener()
        {
            var tcp = new TcpListener(IPAddress.Any, 25565);
            tcp.Start();

            var listeningThread = new Thread(() =>
            {
                while (true)
                {
                    var tcpClient = tcp.AcceptTcpClient();
                    ThreadPool.QueueUserWorkItem(param =>
                    {
                        //Prijem poruke
                        NetworkStream stream = tcpClient.GetStream();
                        string incomming;
                        byte[] bytes = new byte[1024];
                        int i = stream.Read(bytes, 0, bytes.Length);
                        //Primljena poruka je sacuvana u incomming stringu
                        incomming = Encoding.ASCII.GetString(bytes, 0, i);

                        //Ukoliko je primljena poruka pitanje koliko objekata ima u sistemu -> odgovor
                        if (incomming.Equals("Need object count"))
                        {
                            //Response
                            /* Umesto sto se ovde salje count.ToString(), potrebno je poslati 
                             * duzinu liste koja sadrzi sve objekte pod monitoringom, odnosno
                             * njihov ukupan broj (NE BROJATI OD NULE, VEC POSLATI UKUPAN BROJ)
                             * */
                            Byte[] data = Encoding.ASCII.GetBytes(StaticData.DERs.Count.ToString());
                            stream.Write(data, 0, data.Length);
                        }
                        else
                        {
                            //U suprotnom, server je poslao promenu stanja nekog objekta u sistemu
                            Console.WriteLine(incomming); //Na primer: "Entitet_1:272"

                            //################ IMPLEMENTACIJA ####################
                            // Obraditi poruku kako bi se dobile informacije o izmeni
                            // Azuriranje potrebnih stvari u aplikaciji
                            
                            string[] split = incomming.Split(':');
                            int id = Int32.Parse(split[0].Split('_')[1]);
                            double valueMeasure = Double.Parse(split[1]);

                            StaticData.loggedDatas.Add(new LoggedData(StaticData.DERs[id].Id, DateTime.Now, valueMeasure));
                            StaticData.DataIO.SaveLog("Log.txt", new LoggedData(id, DateTime.Now, valueMeasure));

                            StaticData.DERs[id].ValueMeasure = valueMeasure;
                        }
                    }, null);
                }
            });

            listeningThread.IsBackground = true;
            listeningThread.Start();
        }
        #endregion

        #region nav
        public MyICommand<string> NavCommand { get; private set; }
        private MeasurementGraphViewModel measurementGraphViewModel = new MeasurementGraphViewModel();
        private NetworkDisplayViewModel networkDisplayViewModel = new NetworkDisplayViewModel();
        private NetworkEntitiesViewModel networkEntitiesViewModel = new NetworkEntitiesViewModel();
        private BindableBase currentViewModel;

        public BindableBase CurrentViewModel
        {
            get { return currentViewModel; }
            set
            {
                SetProperty(ref currentViewModel, value);
            }
        }

        private List<string> UndoDestinations = new List<string>();

        private void OnNav(string destination)
        {
            UndoDestinations.Add(destination);
            switch (destination)
            {
                case "network entities":
                    CurrentViewModel = networkEntitiesViewModel;
                    break;
                case "network display":
                    CurrentViewModel = networkDisplayViewModel;
                    break;
                case "measurement graph":
                    CurrentViewModel = measurementGraphViewModel;
                    break;
            }
        }

        private void OnUndoNav()
        {
            if(UndoDestinations.Count > 1)
            {
                string destination = UndoDestinations.ElementAt(UndoDestinations.Count - 2);
                switch (destination)
                {
                    case "network entities":
                        CurrentViewModel = networkEntitiesViewModel;
                        break;
                    case "network display":
                        CurrentViewModel = networkDisplayViewModel;
                        break;
                    case "measurement graph":
                        CurrentViewModel = measurementGraphViewModel;
                        break;
                }
                UndoDestinations.RemoveAt(UndoDestinations.Count - 1);
            }
        }
        #endregion

        public MyICommand HomeCommand { get; set; }

        private void OnHome()
        {
            CurrentViewModel = networkEntitiesViewModel;
        }

        public MyICommand ClosingMainWindow { get; set; }

        public void OnClosingMainWindow()
        {
            StaticData.DataIO.SaveData("ders.xml", StaticData.DERs);
        }

        public MyICommand UndoCommand { get; set; }
        public void OnUndo()
        {
            if (StaticData.myICommands.Count > 0)
            {
                ICommandUndo command = StaticData.myICommands.Pop();
                command.UnExecute();
            }
        }
    }
}
