#region Header
//               _,-'/-'/
//   .      __,-; ,'( '/
//    \.    `-.__`-._`:_,-._       _ , . ``
//     `:-._,------' ` _,`--` -: `_ , ` ,' :
//        `---..__,,--'  (C) 2023  ` -'. -'
//        #                Vita-Nex                 #
//  {o)xxx|================-   #   -================|xxx(o}
//        #  https://vita-nex.com/xf-addon-hasher/  #
#endregion

namespace XFAddonHasher
{
	internal static class App
	{
		[STAThread]
		internal static void Main()
		{
			ApplicationConfiguration.Initialize();
			Application.Run(new Interface());
		}
	}
}
