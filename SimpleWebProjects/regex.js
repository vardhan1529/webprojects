//JavaScript-C24.2.0 (SpiderMonkey)


console.log("--- --- ---- phone number test")
var ps = ['123 123 1234', '1234 123 1234', 'ads ad adfa', 'a12 b12 c123'];
for(var i=0; i<ps.length; i++)
{
console.log(ps[i] + ':------------        ' +          /^\d{3}[ ]\d{3}[ ]\d{4}$/.test(ps[i]))
}
console.log("(---) --- ---- phone number test");
ps = ['(123) 123 1234', '(1234) 123 1234', 'ads ad adfa', 'a12 b12 c123','123 231 2134', '(123) 213 12324','(123) 213 1232 3455'];
for(var i=0; i<ps.length; i++)
{
console.log( ps[i] + ':------------        ' +         /^[(]{1}\d{3}[)][ ]\d{3}[ ]\d{4}$/.test(ps[i]))
}

console.log("(---) --- ---- or --- --- ---- phone number test");
ps = ['(123) 123 1234', '(1234) 123 1234', 'ads ad adfa', 'a12 b12 c123','123 231 2134', '(123) 213 12324','(123) 213 1232 3455', '(123)123 123 1234'];
for(var i=0; i<ps.length; i++)
{
console.log(ps[i] + ':------------        ' +       /^(?:\({1}\d{3}\)|\d{3})[ ]\d{3}[ ]\d{4}$/.test(ps[i]));
}

console.log("Accept three spaces only")

ps = ['aaaab b c$23435#  ', 'a b c d e', 'aa b c d', 'a  b  c',];
      
for(var i=0; i<ps.length;i++)
      {
      var res = /^(?:\s*\S+ +\S+ +\S+)(?! +\S+)/.exec(ps[i])
      console.log(ps[i] + '         ' + res)
      }
      
      

      
