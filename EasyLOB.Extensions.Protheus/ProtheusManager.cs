using APConnXControl;
using EasyLOB.Library;
using System;

/*
"Executar como Administrador"

CD \Protheus\API
COPY apconn.dll C:\Windows\System32
COPY apconnxcontrol.ocx C:\Windows\System32
COPY apconn.dll C:\Windows\SysWOW64
COPY apconnxcontrol.ocx C:\Windows\SysWOW64

CD C:\Windows\SysWOW64
REGSVR32 apconnxcontrol.ocx

Properties
    Debugging
         Enable native code debugging
 */

namespace EasyLOB.Extensions.Protheus
{
    public class ProtheusManager
    {
        #region Properties

        protected APConnX Connection = null;

        public string Environment { get; private set; }

        public string Server { get; private set; }

        public int Port { get; private set; }

        public string User { get; private set; }

        public string Password { get; private set; }

        public string Empresa { get; private set; }

        public string Filial { get; private set; }

        public bool IsConnected
        {
            get { return Connection.Connected; }
        }

        public bool IsPrepared { get; private set; }

        public string Error
        {
            get { return Connection.LastError; }
        }

        #endregion Properties

        #region Methods

        public ProtheusManager()
        {
            Connection = new APConnX();
            //Connection.DateFormat = "YYYYMMDD";
            //Connection.ShowErrors = true;

            Environment = ConfigurationHelper.AppSettings<string>("Protheus.Environment");
            Server = ConfigurationHelper.AppSettings<string>("Protheus.Server");
            Port = ConfigurationHelper.AppSettings<int>("Protheus.Port");
            User = ConfigurationHelper.AppSettings<string>("Protheus.User");
            Password = ConfigurationHelper.AppSettings<string>("Protheus.Password");

            Empresa = "";
            Filial = "";

            IsPrepared = false;
        }

        public void AddLogicalParameter(bool parameter)
        {
            Connection.AddLogicalParam(parameter);
        }

        public void AddDateParameter(DateTime parameter)
        {
            Connection.AddDateParamAsDouble(parameter.ToOADate());
        }

        /*
        public void AddDateStringParameter(DateTime parameter)
        {
            //string dateAsString = String.Format("{0:dd/MM/yyyy}", parameter);
            //Connection.AddDateParamAsString(dateAsString);
            //string dateAsString = String.Format("{0:ddMMyyyy}", parameter);
            //Connection.AddDateParamAsString(dateAsString);
            string dateAsString = String.Format("{0:yyyyMMdd}", parameter);
            Connection.AddDateParamAsString(dateAsString);
        }
         */

        public void AddNullParameter()
        {
            Connection.AddNullParam();
        }

        public void AddNumericParameter(double parameter)
        {
            Connection.AddNumericParam(parameter);
        }

        public void AddStringParameter(string parameter)
        {
            Connection.AddStringParam(parameter);
        }

        public bool Call(string function)
        {
            return Connection.CallProc(function);
        }

        public void Clear()
        {
            Connection.Clear();
        }

        public bool Connect(string environment = null)
        {
            environment = String.IsNullOrEmpty(environment) ? ConfigurationHelper.AppSettings<string>("Protheus.Environment") : environment;
            if (Environment != environment)
            {
                Disconnect();
            }

            if (!Connection.Connected || Environment != environment)
            {
                Connection.Environment = environment;
                Connection.Server = Server;
                Connection.Port = Port;
                Connection.User = User;
                Connection.Password = Password;

                Environment = environment;
                Empresa = "";
                Filial = "";

                Connection.Connect();
            }

            return Connection.Connected;
        }

        public bool ConnectAndPrepareEnvironment(string environment = null, string empresa = null, string filial = null)
        {
            if (Connect(environment))
            {
                PrepareEnvironment(empresa, filial);
            }

            return IsConnected && IsPrepared;
        }

        public void Disconnect()
        {
            Connection.Disconnect();

            Environment = "";
            Empresa = "";
            Filial = "";
        }

        public bool PrepareEnvironment(string empresa = null, string filial = null)
        {
            empresa = String.IsNullOrEmpty(empresa) ? ConfigurationHelper.AppSettings<string>("Protheus.Empresa") : empresa;
            filial = String.IsNullOrEmpty(filial) ? ConfigurationHelper.AppSettings<string>("Protheus.Filial") : filial;
            if (!IsPrepared || Empresa != empresa || Filial != filial)
            {
                object[] tabelas = { };

                IsPrepared = Connection.PrepareEnv(Environment, empresa, filial, tabelas);
                if (IsPrepared)
                {
                    Empresa = empresa;
                    Filial = filial;
                }
            }

            return IsPrepared;
        }

        public object ResultToArray()
        {
            return Connection.ResultAsArray();
        }

        public object ResultToDate()
        {
            return Connection.ResultAsDate();
        }

        /*
        public object ResultToDateString()
        {
            return Connection.ResultAsDateString();
        }
         */

        public object ResultToLogical()
        {
            return Connection.ResultAsLogical();
        }

        public object ResultToNumeric()
        {
            return Connection.ResultAsNumeric();
        }

        public object ResultToString()
        {
            return Connection.ResultAsString();
        }

        public char GetResultType()
        {
            /*
            65  A   Array
            66  B   Bloco de Código
            67  C   Caracter
            68  D   Data
            76  L   Lógico
            77  M   Campo memo
            78  N   Numérico
            79  O   Objeto
            85  U   NIL
             */
            short type = Connection.ResultType;

            return Convert.ToChar(type);
        }

        #endregion Methods
    }
}