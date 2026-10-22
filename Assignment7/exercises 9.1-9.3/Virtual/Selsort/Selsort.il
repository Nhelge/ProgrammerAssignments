.method public hidebysig static void  SelectionSort(int32[] arr) cil managed
  {
    // Code size       57 (0x39)
    .maxstack  4
    .locals init (int32 V_0,    // local 0 = i
                  int32 V_1,    // local 1 = least
                  int32 V_2,    // local 2 = j
                  int32 V_3)    // local 3 = tmp
    // argument 0 = arr

    // ---- i = 0, hop til ydre løkke-test ----
    IL_0000:  ldc.i4.0                // push 0
    IL_0001:  stloc.0                 // i = 0
    IL_0002:  br.s       IL_0032      // jump to outer test

    // ---- ydre løkke: least = i; j = i+1 ----
    IL_0004:  ldloc.0                 // push i
    IL_0005:  stloc.1                 // least = i
    IL_0006:  ldloc.0                 // push i
    IL_0007:  ldc.i4.1                // push 1
    IL_0008:  add                     // push i+1
    IL_0009:  stloc.2                 // j = i+1
    IL_000a:  br.s       IL_001a      // jump to inner test

    // ---- indre løkke: if (arr[j] < arr[least]) ----
    IL_000c:  ldarg.0                 // push arr
    IL_000d:  ldloc.2                 // push j
    IL_000e:  ldelem.i4               // push arr[j]
    IL_000f:  ldarg.0                 // push arr
    IL_0010:  ldloc.1                 // push least
    IL_0011:  ldelem.i4               // push arr[least]
    IL_0012:  bge.s      IL_0016      // if arr[j] >= arr[least], skip

    IL_0014:  ldloc.2                 // push j
    IL_0015:  stloc.1                 // least = j

    // ---- j++ ----
    IL_0016:  ldloc.2                 // push j
    IL_0017:  ldc.i4.1                // push 1
    IL_0018:  add                     // push j+1
    IL_0019:  stloc.2                 // j = j+1

    // ---- indre løkke-test: j < arr.Length ----
    IL_001a:  ldloc.2                 // push j
    IL_001b:  ldarg.0                 // push arr
    IL_001c:  ldlen                   // push arr.Length
    IL_001d:  conv.i4                 // convert Length to int32
    IL_001e:  blt.s      IL_000c      // if j < arr.Length, loop inner loop


    // ---- swap: tmp = arr[i]; arr[i] = arr[least]; arr[least] = tmp ----
    IL_0020:  ldarg.0                 // push arr
    IL_0021:  ldloc.0                 // push i
    IL_0022:  ldelem.i4               // push arr[i]
    IL_0023:  stloc.3                 // tmp = arr[i]
    IL_0024:  ldarg.0                 // push arr 
    IL_0025:  ldloc.0                 // push i 
    IL_0026:  ldarg.0                 // push arr
    IL_0027:  ldloc.1                 // push least
    IL_0028:  ldelem.i4               // push arr[least]
    IL_0029:  stelem.i4               // arr[i] = arr[least]
    IL_002a:  ldarg.0                 // push arr
    IL_002b:  ldloc.1                 // push least
    IL_002c:  ldloc.3                 // push tmp
    IL_002d:  stelem.i4               // arr[least] = tmp

    // ---- i++ ----
    IL_002e:  ldloc.0                 // push i
    IL_002f:  ldc.i4.1                // push 1
    IL_0030:  add                     // push i+1
    IL_0031:  stloc.0                 // i = i+1

    // ---- ydre løkke-test: i < arr.Length ----
    IL_0032:  ldloc.0                 // push i
    IL_0033:  ldarg.0                 // push arr
    IL_0034:  ldlen                   // push arr.Length
    IL_0035:  conv.i4                 // convert Length to int32
    IL_0036:  blt.s      IL_0004      // if i < arr.Length, loop outer loop

    IL_0038:  ret                     // return
  } // end of method Selsort::SelectionSort