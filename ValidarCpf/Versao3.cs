using System;
using System.Collections.Generic;
using System.Text;

namespace ValidarCpf
{
    public static class Versao3
    {
        public struct Cpf
        {
            private readonly string _value;

            private Cpf(string value)
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

            public static implicit operator Cpf(string value)
                => new Cpf(value);

            public override string ToString() => _value;
        }

        public static bool ValidarCPF(Cpf sourceCPF)
        {
            if (sourceCPF.CalculaNumeroDeDigitos() != 11)
            {
                return false;
            }

            int totalDigitoI = 0;
            int totalDigitoII = 0;

            if (sourceCPF.VerficarSeTodosOsDigitosSaoIdenticos())
            {
                return false;
            }

            for (int posicao = 0; posicao < 9; posicao++)
            {
                var digito = sourceCPF.ObterDigito(posicao);
                totalDigitoI += digito * (10 - posicao);
                totalDigitoII += digito * (11 - posicao);
            }

            var modI = totalDigitoI % 11;
            if (modI < 2) { modI = 0; }
            else { modI = 11 - modI; }

            if (sourceCPF.ObterDigito(9) != modI)
            {
                return false;
            }

            totalDigitoII += modI * 2;

            var modII = totalDigitoII % 11;
            if (modII < 2) { modII = 0; }
            else { modII = 11 - modII; }

            if (sourceCPF.ObterDigito(10) != modII)
            {
                return false;
            }

            return true;
        }
    }
}
