using System;
using Model;
using Logic;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SimpleCrypto;
using System.Web.Security;

namespace Presentation
{
    public partial class Default : System.Web.UI.Page
    {

        UsersLog objUserLog = new UsersLog();
        User objUser = new User();

        private string _correo;
        private string _contrasena;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnIniciar_Click(object sender, EventArgs e)
        {
            // Muestra la imagen de cargando antes de procesar la encriptación
            ScriptManager.RegisterStartupScript(this, GetType(), "showLoading", "showLoading();", true);

            /*
             * Instancia un servicio de criptografía utilizando el algoritmo PBKDF2 (Password-Based Key Derivation Function 2),
             * que es comúnmente utilizado para cifrar contraseñas de forma segura.
             */

            ICryptoService cryptoService = new PBKDF2();
            _correo = TBCorreo.Text;// Asigna el valor ingresado en el TextBox del correo electrónico 
            _contrasena = TBContrasena.Text;

            objUser = objUserLog.showUsersMail(_correo);// Busca el correo del usuario

            if (objUser != null)
            {
                if (objUser.State == "Activo")// Verifica si el usuario está activo
                {
                    // Almacena objUser en la sesión
                    Session["User"] = objUser;

                    string passEncryp = cryptoService.Compute(_contrasena, objUser.Salt);
                    if (cryptoService.Compare(objUser.Contrasena, passEncryp))
                    {
                        FormsAuthentication.RedirectFromLoginPage("Index.aspx", true);
                        TBCorreo.Text = "";
                        TBContrasena.Text = "";
                    }
                    else
                    {
                        LblMsg.Text = "Correo o Contraseña Incorrectos!";
                    }
                }
                else
                {
                    LblMsg.Text = "El usuario no está activo. Contacte al administrador.";
                }

            }
            else
            {
                LblMsg.Text = "Correo o Contraseña Incorrectos!";
            }
            // Oculta la imagen de cargando al terminar el procesamiento
            ScriptManager.RegisterStartupScript(this, GetType(), "hideLoading", "hideLoading();", true);
        }
    }
}