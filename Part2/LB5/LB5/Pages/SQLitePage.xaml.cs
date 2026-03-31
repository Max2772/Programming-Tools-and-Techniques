using LB5.Entities;
using LB5.Services;

namespace LB5.Pages;

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
        typePicker.ItemsSource = _procedureTypes;
    }

    private void OnTypeSelected(object sender, EventArgs e)
    {
        var selectedType = (ProcedureType)typePicker.SelectedItem;

        if (selectedType != null)
        {
            _procedures = new List<Procedure>(_sqLiteService.GetProceduresByType(selectedType.Id));
            proceduresCollectionView.ItemsSource = _procedures;
            proceduresCollectionView.IsVisible = true;
            emptyStateLabel.IsVisible = false;
        }
    }
}