using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2019 {
    class Day4 {

        public static long GetNumberOfPasswords(int start, int end, bool taskB) {
            long res = 0;
            for (int i = start; i < end; i++) {
                res += IsValid(i, taskB) ? 1 : 0;
            }
            return res;
        }

        static bool IsValid(int password, bool taskB) {
            IEnumerable<int> pw = password.ToString().Select(o => int.Parse(o.ToString()));
            int last = 0;
            foreach (int digit in pw) {
                if (digit >= last) {
                    last = digit;
                    continue;
                }
                return false;
            }
            Regex rx = new Regex(@"([0-9])\1+");
            if (!rx.IsMatch(password.ToString()) || (taskB && rx.Matches(password.ToString()).All(x => x.Length != 2))) {
                return false;
            }
            return true;
        }

    }
}
