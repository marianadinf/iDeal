using System.Collections.Generic;
using System.Linq;
using UIT.iDeal.Common.Builders.DataSources.Base;
using UIT.iDeal.Common.Builders.DataSources.ReferenceData;
using UIT.iDeal.Domain.Model;
using UIT.iDeal.Domain.Model.ReferenceData;

namespace UIT.iDeal.Common.Builders.DataSources.Domain
{
    public class UserDataSource : DatasourceBase<User>
    {
        private ReferenceDataSource<ApplicationRole> _applicationRoleDataSource = new ApplicationRoleReferenceDataSource();
        private ReferenceDataSource<BusinessUnit> _businessUnitDataSource = new BusinessUnitReferenceDataSource();


        public UserDataSource()
        {
            _applicationRoleDataSource.Generator = new RandomListGenerator(1,_applicationRoleDataSource.Count() - 1);
            _businessUnitDataSource.Generator = new RandomListGenerator(1,_businessUnitDataSource.Count() - 1);
        }
        // 100 random names from http://www.generatedata.com/
        protected override List<User> InitialiseList()
        {
            

            return new List<User>
            {
                CreateUser("Maisie", "Fleming", "MFleming", "Suspendisse.commodo.tincidunt@enimNunc.ca"),
                CreateUser("Malik", "Thornton", "MThornton", "ullamcorper.Duis@velvulputate.org"),
                CreateUser("Tatyana", "Hopkins", "THopkins", "non@risusDonecnibh.ca"),
                CreateUser("Joseph", "Mcclure", "JMcclure", "sodales.at.velit@enimnonnisi.edu"),
                CreateUser("Zorita", "Atkins", "ZAtkins", "venenatis@elementum.org"),
                CreateUser("Sasha", "Brennan", "SBrennan", "magna.sed@euplacerateget.edu"),
                CreateUser("Levi", "Webb", "LWebb", "urna.nec.luctus@commodoat.org"),
                CreateUser("Branden", "Head", "BHead", "justo.Proin@molestiearcuSed.ca"),
                CreateUser("Octavius", "Mendoza", "OMendoza", "In@pedenonummy.com"),
                CreateUser("Whitney", "Cooper", "WCooper", "elit.Etiam.laoreet@luctus.com"),
                CreateUser("Emery", "Bailey", "EBailey", "et.euismod@Sedeueros.com"),
                CreateUser("Halla", "Alston", "HAlston", "Donec.tempor.est@etmagnis.edu"),
                CreateUser("Hilel", "Bauer", "HBauer", "Donec@molestiesodalesMauris.ca"),
                CreateUser("Ethan", "Guthrie", "EGuthrie", "lobortis@scelerisquesed.edu"),
                CreateUser("Stella", "Fletcher", "SFletcher", "purus@Cras.edu"),
                CreateUser("Erich", "Ray", "ERay", "dis.parturient@seddictumeleifend.edu"),
                CreateUser("Chantale", "Watts", "CWatts", "cursus@parturient.org"),
                CreateUser("Meredith", "Kent", "MKent", "auctor.vitae@Sedcongueelit.edu"),
                CreateUser("Ira", "Roman", "IRoman", "auctor@hendrerit.com"),
                CreateUser("Hiroko", "Cummings", "HCummings", "Morbi.quis@adipiscingelitAliquam.com"),
                CreateUser("Orli", "Higgins", "OHiggins", "at.risus@metusIn.org"),
                CreateUser("Hakeem", "Chaney", "HChaney", "elementum@Phasellusfermentum.edu"),
                CreateUser("Kay", "Lane", "KLane", "hendrerit@Etiam.com"),
                CreateUser("Heather", "Bruce", "HBruce", "mi.fringilla.mi@libero.org"),
                CreateUser("Ella", "Ferguson", "EFerguson", "magna.Cras.convallis@lectusconvallisest.ca"),
                CreateUser("Audra", "Flynn", "AFlynn", "sit.amet@necante.com"),
                CreateUser("Brenden", "Holden", "BHolden", "sociis.natoque@duinectempus.org"),
                CreateUser("Demetria", "Ferguson", "DFerguson", "parturient.montes.nascetur@idlibero.com"),
                CreateUser("Noelle", "Vaughn", "NVaughn", "adipiscing.elit.Curabitur@blandit.edu"),
                CreateUser("Thor", "Galloway", "TGalloway", "vestibulum.nec.euismod@Integerin.com"),
                CreateUser("Ava", "Blankenship", "ABlankenship", "turpis.Aliquam.adipiscing@nonbibendum.ca"),
                CreateUser("Jamal", "Clarke", "JClarke", "vel.quam@nasceturridiculus.com"),
                CreateUser("Shaine", "White", "SWhite", "malesuada.Integer.id@tristiquealiquetPhasellus.org"),
                CreateUser("Barclay", "Miranda", "BMiranda", "enim.nisl@MaurismagnaDuis.ca"),
                CreateUser("Lara", "Wolf", "LWolf", "lacus.Etiam@penatibus.ca"),
                CreateUser("Lamar", "Randolph", "LRandolph", "dolor.Quisque.tincidunt@turpisnonenim.org"),
                CreateUser("Harper", "Juarez", "HJuarez", "luctus@Nuncac.ca"),
                CreateUser("Fritz", "Oneil", "FOneil", "dolor.elit@nonummy.com"),
                CreateUser("Donna", "Knox", "DKnox", "non.quam@mattisInteger.com"),
                CreateUser("Kirestin", "Byrd", "KByrd", "Donec.fringilla@orciPhasellusdapibus.edu"),
                CreateUser("Riley", "Cross", "RCross", "Curabitur.dictum.Phasellus@ipsumdolorsit.com"),
                CreateUser("Caldwell", "Vargas", "CVargas", "amet@magnaet.ca"),
                CreateUser("Ursa", "Knapp", "UKnapp", "vitae@Ut.edu"),
                CreateUser("Kameko", "Knowles", "KKnowles", "tristique.senectus@liberoMorbi.edu"),
                CreateUser("Karly", "Weeks", "KWeeks", "ligula.Aenean@Phasellus.org"),
                CreateUser("Sonya", "Delgado", "SDelgado", "imperdiet.ullamcorper.Duis@Aenean.com"),
                CreateUser("Price", "Pacheco", "PPacheco", "tortor.Nunc@est.ca"),
                CreateUser("Courtney", "Hernandez", "CHernandez", "Donec.tincidunt.Donec@accumsanconvallisante.ca"),
                CreateUser("Kylan", "Grant", "KGrant", "a.odio.semper@sociis.com"),
                CreateUser("Jessamine", "Mann", "JMann", "sodales.at.velit@Donecelementumlorem.com"),
                CreateUser("Warren", "Wilkins", "WWilkins", "neque.Morbi.quis@nislsemconsequat.com"),
                CreateUser("Stewart", "Waters", "SWaters", "amet@Aliquamadipiscing.ca"),
                CreateUser("Echo", "Newman", "ENewman", "erat.neque@aliquet.ca"),
                CreateUser("Fulton", "Ware", "FWare", "pede@Vestibulumante.edu"),
                CreateUser("Emery", "Bennett", "EBennett", "posuere.at@urna.ca"),
                CreateUser("Nichole", "Mendez", "NMendez", "non.hendrerit@sapienCras.org"),
                CreateUser("Ursa", "Shaw", "UShaw", "in.tempus@idmollis.edu"),
                CreateUser("Katell", "Reilly", "KReilly", "fringilla.cursus.purus@nibhAliquamornare.org"),
                CreateUser("Clementine", "Lambert", "CLambert", "lorem.vehicula.et@Morbimetus.ca"),
                CreateUser("Lucy", "Pittman", "LPittman", "condimentum.eget.volutpat@Pellentesquehabitant.ca"),
                CreateUser("Timon", "Pacheco", "TPacheco", "In.lorem@ultricesiaculis.edu"),
                CreateUser("Allegra", "Davidson", "ADavidson", "eget@etnetuset.com"),
                CreateUser("Naida", "Gould", "NGould", "libero@nequeetnunc.com"),
                CreateUser("Ivan", "England", "IEngland", "facilisis@scelerisquenequesed.com"),
                CreateUser("Garrison", "Vasquez", "GVasquez", "pellentesque@augueeu.edu"),
                CreateUser("Camden", "Vega", "CVega", "sem@nonummy.org"),
                CreateUser("Brynn", "Fischer", "BFischer", "pharetra@Curae;Phasellus.org"),
                CreateUser("Karina", "Carpenter", "KCarpenter", "pharetra@Maurisut.edu"),
                CreateUser("Hope", "Butler", "HButler", "mauris.erat@nec.edu"),
                CreateUser("Melvin", "Kinney", "MKinney", "velit@nondui.edu"),
                CreateUser("Larissa", "Hopkins", "LHopkins", "pretium.neque.Morbi@liberoMorbiaccumsan.org"),
                CreateUser("Haley", "Cain", "HCain", "et@Mauris.ca"),
                CreateUser("Nasim", "Moreno", "NMoreno", "erat.Sed.nunc@tacitisociosquad.ca"),
                CreateUser("Rose", "Vaughn", "RVaughn", "Ut.sagittis.lobortis@ullamcorpereu.com"),
                CreateUser("Shay", "Church", "SChurch", "laoreet@sapienNunc.edu"),
                CreateUser("Gavin", "Walton", "GWalton", "ac.nulla.In@ornare.org"),
                CreateUser("George", "Flynn", "GFlynn", "quam.elementum@natoquepenatibuset.edu"),
                CreateUser("Nina", "Mcdaniel", "NMcdaniel", "nascetur@cursuset.edu"),
                CreateUser("Victor", "Savage", "VSavage", "laoreet@facilisisfacilisismagna.edu"),
                CreateUser("Lucas", "Morrison", "LMorrison", "Fusce.diam@iderat.org"),
                CreateUser("Mechelle", "Washington", "MWashington", "tortor@vel.edu"),
                CreateUser("Chantale", "Bentley", "CBentley", "Quisque@Phasellusvitae.com"),
                CreateUser("Carolyn", "Benjamin", "CBenjamin", "nec.enim@velquamdignissim.ca"),
                CreateUser("Steven", "Frye", "SFrye", "aliquet@eutelluseu.com"),
                CreateUser("Kieran", "Jenkins", "KJenkins", "ut@variusNam.edu"),
                CreateUser("Melissa", "Burt", "MBurt", "morbi@augue.org"),
                CreateUser("Selma", "Morton", "SMorton", "elit@cursusvestibulumMauris.edu"),
                CreateUser("Leonard", "Farmer", "LFarmer", "convallis.est.vitae@nonarcu.ca"),
                CreateUser("Kitra", "Lucas", "KLucas", "Sed.nunc@Donec.com"),
                CreateUser("Irma", "Stewart", "IStewart", "erat.Sed@adipiscingelitEtiam.ca"),
                CreateUser("Charlotte", "Dickson", "CDickson", "convallis.convallis.dolor@loremDonecelementum.edu"),
                CreateUser("Libby", "Hawkins", "LHawkins", "Nulla@odiosagittissemper.org"),
                CreateUser("Dora", "Davis", "DDavis", "ipsum.porta@tellusimperdietnon.ca"),
                CreateUser("Regan", "James", "RJames", "ornare@tristiquealiquet.com"),
                CreateUser("Genevieve", "Madden", "GMadden", "mauris@necimperdietnec.org"),
                CreateUser("Morgan", "Sanchez", "MSanchez", "ac.arcu@orci.com"),
                CreateUser("Talon", "Solomon", "TSolomon", "sagittis@quam.ca"),
                CreateUser("Silas", "Owens", "SOwens", "Suspendisse.tristique.neque@sodales.ca"),
                CreateUser("Kareem", "Wall", "KWall", "blandit@nisiCum.com"),
                CreateUser("Eden", "Kelly", "EKelly", "Proin.mi.Aliquam@Praesent.edu"),
            };



        }

        private User CreateUser(string firstname,
                                  string lastname,
                                  string username,
                                  string email)
        {
            var newUser = User.Create(firstname, lastname, username, email);
            newUser.AddApplicationRoles(GetRandomListFrom(_applicationRoleDataSource));
            newUser.AddBusinessUnits(GetRandomListFrom(_businessUnitDataSource));
            
            return newUser;
        }

        private IEnumerable<T> GetRandomListFrom<T>(ReferenceDataSource<T> dataSource,  int listSize = 3) 
            where T : iDeal.Domain.Model.ReferenceData.ReferenceData, new()
        {
            
            for (var i = 0; i < listSize; i++)
            {
                yield return dataSource.Next();
            }
        }
    }
}
