using System.Linq;
using System.Text;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using UIT.iDeal.Infrastructure.Web.ModelMetaData;

namespace UIT.iDeal.Infrastructure.Bootstrapper.Installers
{
    public class ModelMetaDataInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes
                    .FromThisAssembly()
                    .BasedOn<IModelMetadataFilter>()
                    .WithService.Base()
                    .LifestyleTransient());

            IModelMetadataFilter[] filters = container.ResolveAll<IModelMetadataFilter>();
            ModelMetadataProviders.Current = new CustomModelMetadataProvider(filters);
        }
    }
}
