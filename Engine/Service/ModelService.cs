using Engine.Model;

namespace Engine.Service
{
    public class ModelService<TModel>
        where TModel : IModel
    {
        public virtual TModel Query(long id) {
            return default(TModel);
        }

        // public virtual TModel Query(QueryBuilder builder) {
        //     return default(TModel);
        // }
    }
}