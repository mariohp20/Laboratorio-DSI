using log4net;
using System.Reflection;

namespace MiBotica.SolPedido.Entidades.Base
{
    public class BaseLN  // Cambia internal por public
    {
        protected log4net.ILog Log
        {
            get
            {
                return log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().GetType());
            }
        }
    }
}