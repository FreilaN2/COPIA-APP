using Microsoft.Data.SqlClient;
using Microsoft.Maui.Controls;
using System;
using SpinningTrainer.Model;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;

namespace SpinningTrainer.View
{
    public partial class UsersList : ContentPage
    {        
        ObservableCollection<Usuarios> infoUsuarios = new ObservableCollection<Usuarios>();

        public UsersList()
        {
            InitializeComponent();
            CargarUsuarios();
        }

        private void CargarUsuarios()
        {
            infoUsuarios.Add(new Usuarios { Id=1, CodUsua="Vellito", Descrip="Diego Estevez", Contra="LoMaisimo", PIN="12345678", Email="diegoestevz73@gmail.com", Telef="0424-5712363", FechaC=DateTime.Now, FechaR=DateTime.Now, FechaV=DateTime.Now, TipoUsuario=1 });
            infoUsuarios.Add(new Usuarios { Id=2, CodUsua="Manu", Descrip="Emmanuel Palacios", Contra="LoMaisimo", PIN="12345678", Email="diegoestevz73@gmail.com", Telef="0424-5712363", FechaC=DateTime.Now, FechaR=DateTime.Now, FechaV=DateTime.Now, TipoUsuario=1 });
            infoUsuarios.Add(new Usuarios { Id=3, CodUsua="Usuario1", Descrip="Usuario 1", Contra="LoMaisimo", PIN="12345678", Email="diegoestevz73@gmail.com", Telef="0424-5712363", FechaC=DateTime.Now, FechaR=DateTime.Now, FechaV=DateTime.Now, TipoUsuario=1 });
            infoUsuarios.Add(new Usuarios { Id=4, CodUsua="Usuario2", Descrip="Usuario 2", Contra="LoMaisimo", PIN="12345678", Email="diegoestevz73@gmail.com", Telef="0424-5712363", FechaC=DateTime.Now, FechaR=DateTime.Now, FechaV=DateTime.Now, TipoUsuario=1 });
            infoUsuarios.Add(new Usuarios { Id=5, CodUsua="Usuario3", Descrip="Usuario 3", Contra="LoMaisimo", PIN="12345678", Email="diegoestevz73@gmail.com", Telef="0424-5712363", FechaC=DateTime.Now, FechaR=DateTime.Now, FechaV=DateTime.Now, TipoUsuario=1 });
            infoUsuarios.Add(new Usuarios { Id=6, CodUsua="Usuario4", Descrip="Usuario 4", Contra="LoMaisimo", PIN="12345678", Email="diegoestevz73@gmail.com", Telef="0424-5712363", FechaC=DateTime.Now, FechaR=DateTime.Now, FechaV=DateTime.Now, TipoUsuario=1 });
            infoUsuarios.Add(new Usuarios { Id=7, CodUsua="Usuario5", Descrip="Usuario 5", Contra="LoMaisimo", PIN="12345678", Email="diegoestevz73@gmail.com", Telef="0424-5712363", FechaC=DateTime.Now, FechaR=DateTime.Now, FechaV=DateTime.Now, TipoUsuario=1 });
            infoUsuarios.Add(new Usuarios { Id=7, CodUsua="Usuario5", Descrip="Usuario 5", Contra="LoMaisimo", PIN="12345678", Email="diegoestevz73@gmail.com", Telef="0424-5712363", FechaC=DateTime.Now, FechaR=DateTime.Now, FechaV=DateTime.Now, TipoUsuario=1 });
            infoUsuarios.Add(new Usuarios { Id=7, CodUsua="Usuario5", Descrip="Usuario 5", Contra="LoMaisimo", PIN="12345678", Email="diegoestevz73@gmail.com", Telef="0424-5712363", FechaC=DateTime.Now, FechaR=DateTime.Now, FechaV=DateTime.Now, TipoUsuario=1 });
            infoUsuarios.Add(new Usuarios { Id=7, CodUsua="Usuario5", Descrip="Usuario 5", Contra="LoMaisimo", PIN="12345678", Email="diegoestevz73@gmail.com", Telef="0424-5712363", FechaC=DateTime.Now, FechaR=DateTime.Now, FechaV=DateTime.Now, TipoUsuario=1 });
            infoUsuarios.Add(new Usuarios { Id=7, CodUsua="Usuario5", Descrip="Usuario 5", Contra="LoMaisimo", PIN="12345678", Email="diegoestevz73@gmail.com", Telef="0424-5712363", FechaC=DateTime.Now, FechaR=DateTime.Now, FechaV=DateTime.Now, TipoUsuario=1 });
            infoUsuarios.Add(new Usuarios { Id=7, CodUsua="Usuario5", Descrip="Usuario 5", Contra="LoMaisimo", PIN="12345678", Email="diegoestevz73@gmail.com", Telef="0424-5712363", FechaC=DateTime.Now, FechaR=DateTime.Now, FechaV=DateTime.Now, TipoUsuario=1 });
            infoUsuarios.Add(new Usuarios { Id=7, CodUsua="Usuario5", Descrip="Usuario 5", Contra="LoMaisimo", PIN="12345678", Email="diegoestevz73@gmail.com", Telef="0424-5712363", FechaC=DateTime.Now, FechaR=DateTime.Now, FechaV=DateTime.Now, TipoUsuario=1 });
            infoUsuarios.Add(new Usuarios { Id=7, CodUsua="Usuario5", Descrip="Usuario 5", Contra="LoMaisimo", PIN="12345678", Email="diegoestevz73@gmail.com", Telef="0424-5712363", FechaC=DateTime.Now, FechaR=DateTime.Now, FechaV=DateTime.Now, TipoUsuario=1 });
            infoUsuarios.Add(new Usuarios { Id=7, CodUsua="Usuario5", Descrip="Usuario 5", Contra="LoMaisimo", PIN="12345678", Email="diegoestevz73@gmail.com", Telef="0424-5712363", FechaC=DateTime.Now, FechaR=DateTime.Now, FechaV=DateTime.Now, TipoUsuario=1 });
            infoUsuarios.Add(new Usuarios { Id=7, CodUsua="Usuario5", Descrip="Usuario 5", Contra="LoMaisimo", PIN="12345678", Email="diegoestevz73@gmail.com", Telef="0424-5712363", FechaC=DateTime.Now, FechaR=DateTime.Now, FechaV=DateTime.Now, TipoUsuario=1 });
            infoUsuarios.Add(new Usuarios { Id=7, CodUsua="Usuario5", Descrip="Usuario 5", Contra="LoMaisimo", PIN="12345678", Email="diegoestevz73@gmail.com", Telef="0424-5712363", FechaC=DateTime.Now, FechaR=DateTime.Now, FechaV=DateTime.Now, TipoUsuario=1 });
            infoUsuarios.Add(new Usuarios { Id=7, CodUsua="Usuario5", Descrip="Usuario 5", Contra="LoMaisimo", PIN="12345678", Email="diegoestevz73@gmail.com", Telef="0424-5712363", FechaC=DateTime.Now, FechaR=DateTime.Now, FechaV=DateTime.Now, TipoUsuario=1 });
            infoUsuarios.Add(new Usuarios { Id=7, CodUsua="Usuario5", Descrip="Usuario 5", Contra="LoMaisimo", PIN="12345678", Email="diegoestevz73@gmail.com", Telef="0424-5712363", FechaC=DateTime.Now, FechaR=DateTime.Now, FechaV=DateTime.Now, TipoUsuario=1 });
            infoUsuarios.Add(new Usuarios { Id=7, CodUsua="Usuario5", Descrip="Usuario 5", Contra="LoMaisimo", PIN="12345678", Email="diegoestevz73@gmail.com", Telef="0424-5712363", FechaC=DateTime.Now, FechaR=DateTime.Now, FechaV=DateTime.Now, TipoUsuario=1 });            
		    
            lvUserListView.ItemsSource = infoUsuarios;
        }

        private async void btnAgregarUsuario_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(NewUserView)}");
        }

        private async void EliminarUsers_Clicked(object sender, EventArgs e)
        {
//            var item = lvUserListView.ItemSelected();
            
        }

        private async void UserListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
           /* if (e.Item == null)
                return;

            var selectedUser = (Usuario)e.Item;

            // Abre una nueva página para mostrar los detalles del usuario seleccionado
            await Navigation.PushAsync(new UserDetailsPage(selectedUser));

            // Desmarca la selección del usuario para que se pueda seleccionar nuevamente
            ((ListView)sender).SelectedItem = null;*/
        }
    }    
}
