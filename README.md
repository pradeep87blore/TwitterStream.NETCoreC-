A C# / .NET Core interface to Twitter.

Thanks to https://github.com/karpach/twitter for the code that accesses twitter. This is used as the base. Some changes were needed to adapt this to .NET core and to use proxies. 

Currently, the application just launches and prints out 10 of my most recent tweets to the console. 

Create a folder called Private where the exe is present. 
	The required Consumer key and Consumer secret key are to be placed in a file called "TwitterKeys.keys". The first line is the Consumer key. The second line is the Consumer secret key. No other fields or text should be present in the file. 
		Example of the file:
		tsRrDt8bYmTVT345jPS5ab4Id
		acbde0j5f7rABCUbysssshJOephGZoRbbbbBZDtQfhOhCLC12
	The proxy, if required, needs to be placed in a file called "Proxy.proxy" under the same folder. 
		Example of the file:
		http://192.168.01.01:1234
	I will make it such that the proxy file need not be present but for now, the file is to be present with a valid IP in it. 