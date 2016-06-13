## SearchEverythingCoreCLI
Everything search companion for [Directory Opus](http://www.gpsoft.com.au/) - CLI Core Version

##About
SearchEverything brings filesystem-wide search results to Directory Opus as a collection which can be further manipulated with the full power of the opus. 

##Setup
1. Download SearchEverything binaries or compile the project yourself and extract to a folder (ex. Program Files\Everything)
2. Download "Everything" from [here](http://www.voidtools.com/). Install and run it.
3. Download the Everything SDK [here](http://www.voidtools.com/Everything-SDK.zip).
4. Extract Everything32.dll and Everything64.dll inside the DLL folder to your SearchEverything folder.
5. Add the button code below to your Directory Opus toolbar. You WILL need to edit the paths where the buttons point to.
```xml
<?xml version="1.0"?>
<button display="both" label_pos="right" separate="yes" type="three_button">
   <label>Everything</label>
   <icon1>#default:find</icon1>
   <button display="both" icon_size="large" label_pos="right">
      <label>Everything (Dialog)</label>
      <tip>Search Everything for Specified Keyword(s)</tip>
      <icon1>#default:find</icon1>
      <function type="normal">
         <instruction>@admin</instruction>
         <instruction>C:\Program Files\Everything\SearchEverything.exe &quot;{dlgstring}&quot;</instruction>
      </function>
   </button>
   <button display="both" icon_size="large" label_pos="right">
      <label>Everything (Clipboard)</label>
      <tip>Search Everything for Current Clipboard Text</tip>
      <icon1>#default:find</icon1>
      <function type="normal">
         <instruction>@admin</instruction>
         <instruction>C:\Program Files\Everything\SearchEverything.exe &quot;{clip}&quot;</instruction>
      </function>
   </button>
   <button backcol="none" display="both" icon_size="large" label_pos="right" textcol="none">
      <label>Everything (Program)</label>
      <tip>Starts / Brings Everything to Front</tip>
      <icon1>#find</icon1>
      <function type="normal">
         <instruction>cd C:\Program Files\Everything</instruction>
         <instruction>C:\Program Files\Everything\Everything.exe</instruction>
      </function>
   </button>
</button>
```
##Command Line Usage
```
SearchEverythingCoreCLI [searchstring]
```
