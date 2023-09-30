using Mapster;
using System.Text.Json.Serialization;

namespace Common.Forms
{
    public abstract class BaseForm<TForm, TEntity> : IRegister
        where TForm : class, new()
        where TEntity : class, new()
    {
        private TypeAdapterConfig Config { get; set; }

        [JsonIgnore]
        public int Id { get; set; }
        public TEntity ToEntity()
        {
            return this.Adapt<TEntity>();
        }

        public TEntity ToEntity(TEntity entity)
        {
            return (this as TForm).Adapt(entity);
        }

        public virtual void AddCustomMappings() { }


        protected TypeAdapterSetter<TForm, TEntity> SetFormCustomMappings()
            => Config.ForType<TForm, TEntity>();



        public void Register(TypeAdapterConfig config)
        {
            Config = config;
            AddCustomMappings();
        }

    }
}