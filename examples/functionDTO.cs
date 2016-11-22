using System;
using System.Threading.Tasks;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;

namespace Tutorials
{
   public class FunctionDTOExampleService
   {
        private readonly Web3.Web3 web3;

        public static string ABI = @"[{'constant':false,'inputs':[{'name':'key','type':'bytes32'},{'name':'name','type':'string'},{'name':'description','type':'string'}],'name':'StoreDocument','outputs':[{'name':'success','type':'bool'}],'payable':false,'type':'function'},{'constant':true,'inputs':[{'name':'','type':'bytes32'},{'name':'','type':'uint256'}],'name':'documents','outputs':[{'name':'name','type':'string'},{'name':'description','type':'string'},{'name':'sender','type':'address'}],'payable':false,'type':'function'}]";

        public static string BYTE_CODE = "0x6060604052610528806100126000396000f3606060405260e060020a60003504634a75c0ff811461002957806379c17cc514610139575b610002565b346100025760408051602060046024803582810135601f81018590048502860185019096528585526101989583359593946044949392909201918190840183828082843750506040805160209735808a0135601f81018a90048a0283018a01909352828252969897606497919650602491909101945090925082915084018382808284375094965050505050505060408051608081018252600060608281018281528352835160208181018652838252808501919091529284018290528351908101845285815280830185905233818501528682529181905291822080546001810180835582818380158290116102cf576003028160030283600052602060002091820191016102cf919061036c565b34610002576101ac600435602435600060208190528281526040902080548290811015610002579060005260206000209060030201600050600281015490925060018301915073ffffffffffffffffffffffffffffffffffffffff1683565b604080519115158252519081900360200190f35b6040805173ffffffffffffffffffffffffffffffffffffffff8316918101919091526060808252845460026000196101006001841615020190911604908201819052819060208201906080830190879080156102495780601f1061021e57610100808354040283529160200191610249565b820191906000526020600020905b81548152906001019060200180831161022c57829003601f168201915b50508381038252855460026000196101006001841615020190911604808252602090910190869080156102bd5780601f10610292576101008083540402835291602001916102bd565b820191906000526020600020905b8154815290600101906020018083116102a057829003601f168201915b50509550505050505060405180910390f35b5050509190906000526020600020906003020160008390919091506000820151816000016000509080519060200190828054600181600116156101000203166002900490600052602060002090601f016020900481019282601f1061041b57805160ff19168380011785555b5061044b9291506103e5565b505060028101805473ffffffffffffffffffffffffffffffffffffffff191690556003015b808211156103f957600060008201600050805460018160011615610100020316600290046000825580601f106103cb57505b5060018201600050805460018160011615610100020316600290046000825580601f106103fd5750610347565b601f01602090049060005260206000209081019061039e91905b808211156103f957600081556001016103e5565b5090565b601f01602090049060005260206000209081019061034791906103e5565b8280016001018555821561033b579182015b8281111561033b57825182600050559160200191906001019061042d565b50506020820151816001016000509080519060200190828054600181600116156101000203166002900490600052602060002090601f016020900481019282601f106104aa57805160ff19168380011785555b506104da9291506103e5565b8280016001018555821561049e579182015b8281111561049e5782518260005055916020019190600101906104bc565b5050604091909101516002909101805473ffffffffffffffffffffffffffffffffffffffff19166c01000000000000000000000000928302929092049190911790555060019594505050505056";

        public static  async Task<string> DeployContractAsync(Web3.Web3 web3, string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null) 
        {
            return await web3.Eth.DeployContract.SendRequestAsync(ABI, BYTE_CODE, addressFrom, gas, valueAmount );
        }

        private Contract contract;

        public FunctionDTOExampleService(Web3.Web3 web3, string address)
        {
            this.web3 = web3;
            this.contract = web3.Eth.GetContract(ABI, address);
        }

        public Function GetFunctionStoreDocument() {
            return contract.GetFunction("StoreDocument");
        }
        public Function GetFunctionDocuments() {
            return contract.GetFunction("documents");
        }


        public async Task<bool> StoreDocumentAsyncCall(byte[] key, string name, string description) {
            var function = GetFunctionStoreDocument();
            return function.CallAsync<bool>(key, name, description);
        }

        public async Task<string> StoreDocumentAsync(string addressFrom, byte[] key, string name, string description, HexBigInteger gas = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionStoreDocument();
            return function.SendTransactionAsync(addressFrom, gas, valueAmount, key, name, description);
        }

        public async Task<StoreDocumentDTO> StoreDocumentAsyncCall(byte[] key, string name, string description) {
            var function = GetFunctionStoreDocument();
            return function.CallDeserializingToObjectAsync<StoreDocumentDTO>(key, name, description);
        }
        public async Task<DocumentsDTO> DocumentsAsyncCall(byte[] a, BigInteger b) {
            var function = GetFunctionDocuments();
            return function.CallDeserializingToObjectAsync<DocumentsDTO>(a, b);
        }


    }

    [FunctionOutput]
    public class DocumentsDTO 
    {
        [Parameter("string", "name", 1)]
        public string Name {get; set;}

        [Parameter("string", "description", 2)]
        public string Description {get; set;}

        [Parameter("address", "sender", 3)]
        public string Sender {get; set;}

    }



}

