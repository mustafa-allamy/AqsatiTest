using Mapster;

namespace Common.Dto
{
    public abstract class BaseDto<TDto, TEntity> : IRegister
        where TDto : class, new()
        where TEntity : class, new()
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }




        public static TDto FromEntity(TEntity entity)
        {
            return entity.Adapt<TDto>();
        }


        private TypeAdapterConfig Config { get; set; }

        public virtual void AddCustomMappings() { }



        protected TypeAdapterSetter<TEntity, TDto> SetDtoCustomMappings()
            => Config.ForType<TEntity, TDto>();

        public void Register(TypeAdapterConfig config)
        {
            Config = config;
            AddCustomMappings();
        }
    }
}