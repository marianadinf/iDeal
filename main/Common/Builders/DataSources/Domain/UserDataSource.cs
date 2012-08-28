using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIT.iDeal.Common.Builders.DataSources.Base;
using UIT.iDeal.Domain.Model;

namespace UIT.iDeal.Common.Builders.DataSources.Domain
{
    public class UserDataSource : DatasourceBase<User>
    {
        // 100 random names from http://www.generatedata.com/
        protected override List<User> InitialiseList()
        {
            
            
            return new List<User>
            {
                User.Create("Maisie", "Fleming", "MFleming", "Suspendisse.commodo.tincidunt@enimNunc.ca"),
                User.Create("Malik", "Thornton", "MThornton", "ullamcorper.Duis@velvulputate.org"),
                User.Create("Tatyana", "Hopkins", "THopkins", "non@risusDonecnibh.ca"),
                User.Create("Joseph", "Mcclure", "JMcclure", "sodales.at.velit@enimnonnisi.edu"),
                User.Create("Zorita", "Atkins", "ZAtkins", "venenatis@elementum.org"),
                User.Create("Sasha", "Brennan", "SBrennan", "magna.sed@euplacerateget.edu"),
                User.Create("Levi", "Webb", "LWebb", "urna.nec.luctus@commodoat.org"),
                User.Create("Branden", "Head", "BHead", "justo.Proin@molestiearcuSed.ca"),
                User.Create("Octavius", "Mendoza", "OMendoza", "In@pedenonummy.com"),
                User.Create("Whitney", "Cooper", "WCooper", "elit.Etiam.laoreet@luctus.com"),
                User.Create("Emery", "Bailey", "EBailey", "et.euismod@Sedeueros.com"),
                User.Create("Halla", "Alston", "HAlston", "Donec.tempor.est@etmagnis.edu"),
                User.Create("Hilel", "Bauer", "HBauer", "Donec@molestiesodalesMauris.ca"),
                User.Create("Ethan", "Guthrie", "EGuthrie", "lobortis@scelerisquesed.edu"),
                User.Create("Stella", "Fletcher", "SFletcher", "purus@Cras.edu"),
                User.Create("Erich", "Ray", "ERay", "dis.parturient@seddictumeleifend.edu"),
                User.Create("Chantale", "Watts", "CWatts", "cursus@parturient.org"),
                User.Create("Meredith", "Kent", "MKent", "auctor.vitae@Sedcongueelit.edu"),
                User.Create("Ira", "Roman", "IRoman", "auctor@hendrerit.com"),
                User.Create("Hiroko", "Cummings", "HCummings", "Morbi.quis@adipiscingelitAliquam.com"),
                User.Create("Orli", "Higgins", "OHiggins", "at.risus@metusIn.org"),
                User.Create("Hakeem", "Chaney", "HChaney", "elementum@Phasellusfermentum.edu"),
                User.Create("Kay", "Lane", "KLane", "hendrerit@Etiam.com"),
                User.Create("Heather", "Bruce", "HBruce", "mi.fringilla.mi@libero.org"),
                User.Create("Ella", "Ferguson", "EFerguson", "magna.Cras.convallis@lectusconvallisest.ca"),
                User.Create("Audra", "Flynn", "AFlynn", "sit.amet@necante.com"),
                User.Create("Brenden", "Holden", "BHolden", "sociis.natoque@duinectempus.org"),
                User.Create("Demetria", "Ferguson", "DFerguson", "parturient.montes.nascetur@idlibero.com"),
                User.Create("Noelle", "Vaughn", "NVaughn", "adipiscing.elit.Curabitur@blandit.edu"),
                User.Create("Thor", "Galloway", "TGalloway", "vestibulum.nec.euismod@Integerin.com"),
                User.Create("Ava", "Blankenship", "ABlankenship", "turpis.Aliquam.adipiscing@nonbibendum.ca"),
                User.Create("Jamal", "Clarke", "JClarke", "vel.quam@nasceturridiculus.com"),
                User.Create("Shaine", "White", "SWhite", "malesuada.Integer.id@tristiquealiquetPhasellus.org"),
                User.Create("Barclay", "Miranda", "BMiranda", "enim.nisl@MaurismagnaDuis.ca"),
                User.Create("Lara", "Wolf", "LWolf", "lacus.Etiam@penatibus.ca"),
                User.Create("Lamar", "Randolph", "LRandolph", "dolor.Quisque.tincidunt@turpisnonenim.org"),
                User.Create("Harper", "Juarez", "HJuarez", "luctus@Nuncac.ca"),
                User.Create("Fritz", "Oneil", "FOneil", "dolor.elit@nonummy.com"),
                User.Create("Donna", "Knox", "DKnox", "non.quam@mattisInteger.com"),
                User.Create("Kirestin", "Byrd", "KByrd", "Donec.fringilla@orciPhasellusdapibus.edu"),
                User.Create("Riley", "Cross", "RCross", "Curabitur.dictum.Phasellus@ipsumdolorsit.com"),
                User.Create("Caldwell", "Vargas", "CVargas", "amet@magnaet.ca"),
                User.Create("Ursa", "Knapp", "UKnapp", "vitae@Ut.edu"),
                User.Create("Kameko", "Knowles", "KKnowles", "tristique.senectus@liberoMorbi.edu"),
                User.Create("Karly", "Weeks", "KWeeks", "ligula.Aenean@Phasellus.org"),
                User.Create("Sonya", "Delgado", "SDelgado", "imperdiet.ullamcorper.Duis@Aenean.com"),
                User.Create("Price", "Pacheco", "PPacheco", "tortor.Nunc@est.ca"),
                User.Create("Courtney", "Hernandez", "CHernandez", "Donec.tincidunt.Donec@accumsanconvallisante.ca"),
                User.Create("Kylan", "Grant", "KGrant", "a.odio.semper@sociis.com"),
                User.Create("Jessamine", "Mann", "JMann", "sodales.at.velit@Donecelementumlorem.com"),
                User.Create("Warren", "Wilkins", "WWilkins", "neque.Morbi.quis@nislsemconsequat.com"),
                User.Create("Stewart", "Waters", "SWaters", "amet@Aliquamadipiscing.ca"),
                User.Create("Echo", "Newman", "ENewman", "erat.neque@aliquet.ca"),
                User.Create("Fulton", "Ware", "FWare", "pede@Vestibulumante.edu"),
                User.Create("Emery", "Bennett", "EBennett", "posuere.at@urna.ca"),
                User.Create("Nichole", "Mendez", "NMendez", "non.hendrerit@sapienCras.org"),
                User.Create("Ursa", "Shaw", "UShaw", "in.tempus@idmollis.edu"),
                User.Create("Katell", "Reilly", "KReilly", "fringilla.cursus.purus@nibhAliquamornare.org"),
                User.Create("Clementine", "Lambert", "CLambert", "lorem.vehicula.et@Morbimetus.ca"),
                User.Create("Lucy", "Pittman", "LPittman", "condimentum.eget.volutpat@Pellentesquehabitant.ca"),
                User.Create("Timon", "Pacheco", "TPacheco", "In.lorem@ultricesiaculis.edu"),
                User.Create("Allegra", "Davidson", "ADavidson", "eget@etnetuset.com"),
                User.Create("Naida", "Gould", "NGould", "libero@nequeetnunc.com"),
                User.Create("Ivan", "England", "IEngland", "facilisis@scelerisquenequesed.com"),
                User.Create("Garrison", "Vasquez", "GVasquez", "pellentesque@augueeu.edu"),
                User.Create("Camden", "Vega", "CVega", "sem@nonummy.org"),
                User.Create("Brynn", "Fischer", "BFischer", "pharetra@Curae;Phasellus.org"),
                User.Create("Karina", "Carpenter", "KCarpenter", "pharetra@Maurisut.edu"),
                User.Create("Hope", "Butler", "HButler", "mauris.erat@nec.edu"),
                User.Create("Melvin", "Kinney", "MKinney", "velit@nondui.edu"),
                User.Create("Larissa", "Hopkins", "LHopkins", "pretium.neque.Morbi@liberoMorbiaccumsan.org"),
                User.Create("Haley", "Cain", "HCain", "et@Mauris.ca"),
                User.Create("Nasim", "Moreno", "NMoreno", "erat.Sed.nunc@tacitisociosquad.ca"),
                User.Create("Rose", "Vaughn", "RVaughn", "Ut.sagittis.lobortis@ullamcorpereu.com"),
                User.Create("Shay", "Church", "SChurch", "laoreet@sapienNunc.edu"),
                User.Create("Gavin", "Walton", "GWalton", "ac.nulla.In@ornare.org"),
                User.Create("George", "Flynn", "GFlynn", "quam.elementum@natoquepenatibuset.edu"),
                User.Create("Nina", "Mcdaniel", "NMcdaniel", "nascetur@cursuset.edu"),
                User.Create("Victor", "Savage", "VSavage", "laoreet@facilisisfacilisismagna.edu"),
                User.Create("Lucas", "Morrison", "LMorrison", "Fusce.diam@iderat.org"),
                User.Create("Mechelle", "Washington", "MWashington", "tortor@vel.edu"),
                User.Create("Chantale", "Bentley", "CBentley", "Quisque@Phasellusvitae.com"),
                User.Create("Carolyn", "Benjamin", "CBenjamin", "nec.enim@velquamdignissim.ca"),
                User.Create("Steven", "Frye", "SFrye", "aliquet@eutelluseu.com"),
                User.Create("Kieran", "Jenkins", "KJenkins", "ut@variusNam.edu"),
                User.Create("Melissa", "Burt", "MBurt", "morbi@augue.org"),
                User.Create("Selma", "Morton", "SMorton", "elit@cursusvestibulumMauris.edu"),
                User.Create("Leonard", "Farmer", "LFarmer", "convallis.est.vitae@nonarcu.ca"),
                User.Create("Kitra", "Lucas", "KLucas", "Sed.nunc@Donec.com"),
                User.Create("Irma", "Stewart", "IStewart", "erat.Sed@adipiscingelitEtiam.ca"),
                User.Create("Charlotte", "Dickson", "CDickson", "convallis.convallis.dolor@loremDonecelementum.edu"),
                User.Create("Libby", "Hawkins", "LHawkins", "Nulla@odiosagittissemper.org"),
                User.Create("Dora", "Davis", "DDavis", "ipsum.porta@tellusimperdietnon.ca"),
                User.Create("Regan", "James", "RJames", "ornare@tristiquealiquet.com"),
                User.Create("Genevieve", "Madden", "GMadden", "mauris@necimperdietnec.org"),
                User.Create("Morgan", "Sanchez", "MSanchez", "ac.arcu@orci.com"),
                User.Create("Talon", "Solomon", "TSolomon", "sagittis@quam.ca"),
                User.Create("Silas", "Owens", "SOwens", "Suspendisse.tristique.neque@sodales.ca"),
                User.Create("Kareem", "Wall", "KWall", "blandit@nisiCum.com"),
                User.Create("Eden", "Kelly", "EKelly", "Proin.mi.Aliquam@Praesent.edu"),
            };



        }
    }
}
