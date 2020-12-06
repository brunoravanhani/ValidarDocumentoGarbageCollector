using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace ValidarCpf
{
    public static class Versao2
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ObterDigito(
            string value,
            int pos
        ) => value[pos] - '0';

        public static bool ValidarCPF(string sourceCPF)
        {
            if (String.IsNullOrWhiteSpace(sourceCPF))
                return false;

            var clearCPF = sourceCPF.Trim();
            clearCPF = clearCPF.Replace("-", "");
            clearCPF = clearCPF.Replace(".", "");

            if (clearCPF.Length != 11)
            {
                return false;
            }

            int totalDigitoI = 0;
            int totalDigitoII = 0;

            if (clearCPF.Equals("00000000000") ||
                clearCPF.Equals("11111111111") ||
                clearCPF.Equals("22222222222") ||
                clearCPF.Equals("33333333333") ||
                clearCPF.Equals("44444444444") ||
                clearCPF.Equals("55555555555") ||
                clearCPF.Equals("66666666666") ||
                clearCPF.Equals("77777777777") ||
                clearCPF.Equals("88888888888") ||
                clearCPF.Equals("99999999999"))
            {
                return false;
            }

            foreach (char c in clearCPF)
            {
                if (!char.IsNumber(c))
                {
                    return false;
                }
            }
            for (int posicao = 0; posicao < clearCPF.Length - 2; posicao++)
            {
                totalDigitoI += ObterDigito(clearCPF, posicao) * (10 - posicao);
                totalDigitoII += ObterDigito(clearCPF, posicao) * (11 - posicao);
            }

            var modI = totalDigitoI % 11;
            if (modI < 2) { modI = 0; }
            else { modI = 11 - modI; }

            if (ObterDigito(clearCPF, 9) != modI)
            {
                return false;
            }

            totalDigitoII += modI * 2;
            var modII = totalDigitoII % 11;
            if (modII < 2) { modII = 0; }
            else { modII = 11 - modII; }

            if (ObterDigito(clearCPF, 10) != modII)
            {
                return false;
            }
            return true;
        }
    }
}
