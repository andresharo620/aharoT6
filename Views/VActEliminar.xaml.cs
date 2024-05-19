using aharoT6.Models;
using System.Net;

namespace aharoT6.Views;

public partial class VActEliminar : ContentPage
{
	public VActEliminar(Estudiante datos)
	{
		InitializeComponent();
        txtCodigo.Text = datos.codigo.ToString();
        txtNombre.Text = datos.nombre.ToString();
        txtApellido.Text = datos.apellido.ToString();
        txtEdad.Text = datos.edad.ToString();
	}

    private async void btbActualiza_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Obtener los valores del formulario
            string codigo = txtCodigo.Text;
            string nombre = txtNombre.Text;
            string apellido = txtApellido.Text;
            string edad = txtEdad.Text;

            if (string.IsNullOrEmpty(codigo) || string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido) || string.IsNullOrEmpty(edad))
            {
                await DisplayAlert("Alerta", "Debe completar todos los campos.", "Cerrar");
                return;
            }

            // Crear un objeto HttpClient
            HttpClient client = new HttpClient();

            // Crear un diccionario con los par�metros
            var parametros = new Dictionary<string, string>
        {
            { "codigo", codigo },
            { "nombre", nombre },
            { "apellido", apellido },
            { "edad", edad }
        };

            // Convertir los par�metros a formato query string
            var content = new FormUrlEncodedContent(parametros);

            // Crear la URL
            string url = $"http://192.168.100.28/moviles/wsestudiantes.php?codigo={codigo}";

            // Crear una solicitud HttpRequestMessage
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, url)
            {
                Content = content
            };

            // Enviar la solicitud PUT
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("�xito", "Registro actualizado correctamente.", "Cerrar");
                await Navigation.PushAsync(new VEstudiante());
            }
            else
            {
                await DisplayAlert("Error", "No se pudo actualizar el registro.", "Cerrar");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Alerta", ex.Message, "Cerrar");
        }
    }

    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Obtener el c�digo del registro a eliminar
            string codigo = txtCodigo.Text;

            if (string.IsNullOrEmpty(codigo))
            {
                await DisplayAlert("Alerta", "Debe ingresar el c�digo del estudiante a eliminar.", "Cerrar");
                return;
            }

            HttpClient client = new HttpClient();

            // Crear la URL con el c�digo como par�metro
            string url = $"http://192.168.100.28/moviles/wsestudiantes.php?codigo={codigo}";

            // Enviar la solicitud DELETE
            HttpResponseMessage response = await client.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("�xito", "Registro eliminado correctamente.", "Cerrar");
                await Navigation.PushAsync(new VEstudiante());
            }
            else
            {
                await DisplayAlert("Error", "No se pudo eliminar el registro.", "Cerrar");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Alerta", ex.Message, "Cerrar");
        }
    }
}