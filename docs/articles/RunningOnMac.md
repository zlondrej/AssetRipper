# Running on mac
In order to run Asset Ripper on mac, you'll first need to have everything listed in the [requirements](https://assetripper.github.io/AssetRipper/articles/Requirements.html) tab.

## Running the GUI version
If you're done with the requirements, lets see how to run the GUI version first.

First, you'll need to download the GUI release from [here](https://github.com/AssetRipper/AssetRipper/releases).

Make sure you download the `AssetRipperGUI_mac64.zip` file as shown below.

![001](Images/RunningOnMac/001.png)

After downloading, right click and open the file to extract as shown below.

![002](Images/RunningOnMac/002.png)

After extracting, you should see this folder named `AssetRipperGUI_mac64`.

![003](Images/RunningOnMac/003.png)

And inside that folder, there should be many other files:

![004](Images/RunningOnMac/004.png)

You should also see this file called `AssetRipper`.

![005](Images/RunningOnMac/005.png)

Now go back to the `AssetRipperGUI_mac64` folder and select it. Then go to `Finder`, `Services`, `New Terminal at Folder` as shown.

![006](Images/RunningOnMac/006.png)

Now you should see the terminal open:

![007](Images/RunningOnMac/007.png)

Now you'll need to type in the following command and press `enter`:

```
./AssetRipper
```

Then you should see an error saying `-bash: ./AssetRipper: Permission denied`.

![008](Images/RunningOnMac/008.png)

To fix that, you'll need to type in this command:

```
chmod +x AssetRipper
```
After entering that, you should see an empty line

![009](Images/RunningOnMac/009.png)

Now again try the following command and it should work.

```
./AssetRipper
```

Now something like this should pop up:

![010](Images/RunningOnMac/010.png)

Click cancel. To verify that, you'll need to open system preferences using `⌘ + space` and search for `System preferences` and press `enter`. Then inside, find for `Security & Privacy` as shown below:

![011](Images/RunningOnMac/011.png)

Now go to the `General` tab:

![012](Images/RunningOnMac/012.png)

And at the bottom, something like this should popup. Click Allow Anyway:

![013](Images/RunningOnMac/013.png)

Now again try this command:

```
./AssetRipper
```

Then it will show something like this, click Open:

![014](Images/RunningOnMac/014.png)

Then things like this will popup, click OK and do the same steps shown above to open them:

![015](Images/RunningOnMac/015.png)

![016](Images/RunningOnMac/016.png)

Like that you should see multiple popups, click OK and allow them.

After a while, you should see Asset Ripper GUI open:

![017](Images/RunningOnMac/017.png)

Now lets say you got it opened, but you closed it and want to open it again. But luckily, you won't need to follow all the steps above to open again!

Just go to the `AssetRipperGUI_mac64` folder and double click this file and it should open again!

![018](Images/RunningOnMac/018.png)

And there you have it :)

![019](Images/RunningOnMac/019.png)
