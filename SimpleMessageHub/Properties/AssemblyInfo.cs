#region License
// Copyright (c) 2019 John C. Gilliland
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
#endregion

using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

[assembly: AllowPartiallyTrustedCallers]


#if !SIGNED

    [assembly: InternalsVisibleTo("SimpleMessageHub.Tests")]

#else

    [assembly: InternalsVisibleTo("SimpleMessageHub.Tests, PublicKey=0024000004800000940000000602000000240000525341310004000001000100fd6f9f9a60f617363e4c04c822f67af319ae302732fdffc255df0a46c97e9788d8495ab3d4029db4d3023e89c134b488e6c91522172c452915ab94fcd88a0ae97b403784527a2368f479c0a50e917316b4958c103d166eb7e23fab4d596d357efa782d5ae39c3dd32206f093c7f9da57d81843d10c7e62fa74c56bf74b9286a2")]

#endif


[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: CLSCompliant(true)]

