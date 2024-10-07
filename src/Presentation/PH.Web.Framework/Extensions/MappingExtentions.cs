namespace PH.Web.Framework;

public static class MappingExtensions
{
    public static TViewModel ToViewModel<TEntity, TViewModel>(this TEntity entity)
        where TEntity : class
        where TViewModel : class, new()
    {
        TViewModel viewModel = new TViewModel();

        var entityProperties = typeof(TEntity).GetProperties();
        var viewModelProperties = typeof(TViewModel).GetProperties();

        foreach (var entityProp in entityProperties)
        {
            var viewModelProp = viewModelProperties
                .FirstOrDefault(p => p.Name == entityProp.Name && p.PropertyType == entityProp.PropertyType);

            if (viewModelProp != null && viewModelProp.CanWrite)
            {
                viewModelProp.SetValue(viewModel, entityProp.GetValue(entity));
            }
        }

        return viewModel;
    }

    public static TEntity ToEntity<TEntity, TViewModel>(this TViewModel viewModel)
        where TViewModel : class
        where TEntity : class, new()
    {
        TEntity entity = new TEntity();

        var viewModelProperties = typeof(TViewModel).GetProperties();
        var entityProperties = typeof(TEntity).GetProperties();

        foreach (var viewModelProp in viewModelProperties)
        {
            var entityProp = entityProperties
                .FirstOrDefault(p => p.Name == viewModelProp.Name && p.PropertyType == viewModelProp.PropertyType);

            if (entityProp != null && entityProp.CanWrite)
            {
                entityProp.SetValue(entity, viewModelProp.GetValue(viewModel));
            }
        }

        return entity;
    }

    public static List<TViewModel> ToViewModelList<TEntity, TViewModel>(this List<TEntity> entities)
        where TEntity : class
        where TViewModel : class, new()
    {
        List<TViewModel> viewModelList = new List<TViewModel>();

        foreach (var entity in entities)
        {
            var viewModel = entity.ToViewModel<TEntity, TViewModel>();
            viewModelList.Add(viewModel);
        }

        return viewModelList;
    }
}