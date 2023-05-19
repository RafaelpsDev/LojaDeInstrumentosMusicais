using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaDeInstrumentosMusicais.Tests.Unit.Utils
{
    public static class AutoFixtureExtensions
    {
            /// <summary>
            /// O AutoFixture tem um comportamento de recursão em algumas entidades relacionadas, essa extensão ajuda a eliminar isso
            /// </summary>
            /// <param name="fixture"></param>
            public static void OmitirComportamentoRecursivo(this IFixture fixture)
            {
                fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(x => fixture.Behaviors.Remove(x));
                fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            }
        }
    }

