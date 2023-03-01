using ApiTeste.Model;
using Microsoft.AspNetCore.Mvc;

namespace ApiTeste.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculoController : ControllerBase
    {

        [HttpGet]
        public Calculo GetCalculo(decimal VI, int qtdmes) {

            decimal ValorInicial = VI;
            //VF = VI x [1 + (CDI x TB)]
            decimal VF = 0;
            decimal CDI = decimal.Parse("0,9") / 100;
            //CDI = 0,9%
            decimal TB = (decimal.Parse("108") / 100);
            //TB = 108%


            for (int i = 0; i < qtdmes; i++)
            {
                VF = VI * (1 + (CDI * TB));
                VI = VF;
            }
            

            Calculo calculo = new Calculo();
            calculo.ValorBruto = Math.Round(VF, 2);

            decimal valorTaxa = 0M;

            switch (qtdmes)
            {
                case <= 6:
                    valorTaxa = 22.5M;
                    break;
                case <= 12:
                    valorTaxa = 20M;
                    break;
                case <= 24:
                    valorTaxa = 17.5M;
                    break;
                default:
                    valorTaxa = 15M;
                    break;
            }

            calculo.ValorLiquido = ValorInicial + Math.Round((calculo.ValorBruto - ValorInicial) * ((100 - valorTaxa) / 100), 2);

            return calculo;
        }
    }
}
