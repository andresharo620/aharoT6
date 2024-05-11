using aharoT6.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Text.Json.Nodes;

namespace aharoT6.Views;

public partial class VEstudiante : ContentPage
{
	private const string url = "http://172.18.112.1/moviles/wsestudiantes.php";
	private readonly HttpClient cliente = new HttpClient();
	private ObservableCollection<Estudiante> est;

	public VEstudiante()
	{
		InitializeComponent();
		ObtenerDatos();

    }

	public async void ObtenerDatos()
	{
		var content = await cliente.GetStringAsync(url);
		List <Estudiante>  mostrar  = JsonConvert.DeserializeObject<List<Estudiante>>(content);
		est= new ObservableCollection<Estudiante>(mostrar);
		listEstudiante.ItemsSource = est;
	}
}