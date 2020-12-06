using System;
using System.Collections.Generic;
using System.Text;

namespace ValidarCnpj
{
    public static class Versao3
    {
        public struct Cnpj
        {
            private readonly string _value;

            public Cnpj(string value)
            {
                _value = value;
            }

            public int CalculaNumeroDeDigitos()
            {
                if (_value == null)
                {
                    return 0;
                }

                var result = 0;
                for (var i = 0; i < _value.Length; i++)
                {
                    if (char.IsDigit(_value[i]))
                    {
                        result++;
                    }
                }

                return result;
            }


            public bool VerficarSeTodosOsDigitosSaoIdenticos()
            {
                var previous = -1;
                for (var i = 0; i < _value.Length; i++)
                {
                    if (char.IsDigit(_value[i]))
                    {
                        var digito = _value[i] - '0';
                        if (previous == -1)
                        {
                            previous = digito;
                        }
                        else
                        {
                            if (previous != digito)
                            {
                                return false;
                            }
                        }
                    }
                }
                return true;
            }

            public int ObterDigito(int posicao)
            {
                int count = 0;
                for (int i = 0; i < _value.Length; i++)
                {
                    if (char.IsDigit(_value[i]))
                    {
                        if (count == posicao)
                        {
                            return _value[i] - '0';
                        }
                        count++;
                    }
                }

                return 0;
            }

            public static implicit operator Cnpj(string value)
                => new Cnpj(value);

            public override string ToString()
                => _value;
        }

        static readonly int[] multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        static readonly int[] multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        public static bool ValidarCNPJ(Cnpj cnpj)
        {
            if (cnpj.CalculaNumeroDeDigitos() != 14)
                return false;

            // Verifica os Patterns mais Comuns para CNPJ's Inválidos
            if (cnpj.VerficarSeTodosOsDigitosSaoIdenticos())
            {
                return false;
            }

            var soma1 = 0;
            var soma2 = 0;
            for (var i = 0; i < 12; i++)
            {
                var d = cnpj.ObterDigito(i);
                soma1 += d * multiplicador1[i];
                soma2 += d * multiplicador2[i];
            }

            var resto = (soma1 % 11);

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            var dv1 = resto;
            //var digito = resto.ToString();
            soma2 += resto * multiplicador2[12];

            resto = (soma2 % 11);

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            var dv2 = resto;

            return cnpj.ObterDigito(12) == dv1 && cnpj.ObterDigito(13) == dv2;
        }
    }
}
