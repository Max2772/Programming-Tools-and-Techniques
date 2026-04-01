using LB6.Entities.Database;
using LB6.Services.Database;

namespace LB6.Pages;

public partial class SQLitePage : ContentPage
{
    private readonly IDbService _sqLiteService;
    private List<ProcedureType> _procedureTypes;
    private List<Procedure> _procedures;

    public SQLitePage(IDbService dbService)
    {
        InitializeComponent();

        _sqLiteService = dbService;
        _sqLiteService.Init();
    }

    private void LoadProcedureTypes(object sender, EventArgs e)
    {
        _procedureTypes = new List<ProcedureType>(_sqLiteService.GetAllProcedureTypes());
        TypePicker.ItemsSource = _procedureTypes;
    }

    private void OnTypeSelected(object sender, EventArgs e)
    {
        var selectedType = (ProcedureType)TypePicker.SelectedItem;

        if (selectedType != null)
        {
            _procedures = new List<Procedure>(_sqLiteService.GetProceduresByType(selectedType.Id));
            ProceduresCollectionView.ItemsSource = _procedures;
            ProceduresCollectionView.IsVisible = true;
            EmptyStateLabel.IsVisible = false;
            TypePicker.Title = "";
        }
    }
}