﻿// 
// Copyright (c) 2013 Jason Bell
// 
// Permission is hereby granted, free of charge, to any person obtaining a 
// copy of this software and associated documentation files (the "Software"), 
// to deal in the Software without restriction, including without limitation 
// the rights to use, copy, modify, merge, publish, distribute, sublicense, 
// and/or sell copies of the Software, and to permit persons to whom the 
// Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included 
// in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS 
// OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.
// 

using System;

namespace Loonim{
	
	public static class GradientNoise{
		
		private const int XNoiseGen = 1619;
		private const int YNoiseGen = 31337;
		private const int ZNoiseGen = 16029;
		private const int SeedNoiseGen = 1013;
		private const int ShiftNoiseGen = 8;

// The following is generated by the NoiseVectorTable GT helper program.
private static double[] RandomVectors = {
0.623016,0.327492,-0.710352,0,
0.140913,0.656037,-0.741457,0,
-0.093938,-0.954385,-0.283418,0,
0.409831,-0.361444,-0.837494,0,
0.0921,-0.443188,0.891685,0,
0.768499,-0.544558,0.335954,0,
-0.392602,-0.636166,0.664196,0,
-0.801925,0.089335,0.590707,0,
-0.406469,0.088709,-0.909348,0,
0.047278,0.653591,0.75537,0,
-0.462649,0.801314,0.379278,0,
0.440519,-0.89774,-0.002325,0,
0.798036,-0.385581,-0.463104,0,
-0.821886,-0.558195,0.113673,0,
-0.515139,-0.504191,-0.693126,0,
0.607757,-0.21447,0.764613,0,
0.792888,0.308174,-0.525697,0,
-0.761932,0.384277,-0.521336,0,
-0.649158,-0.540193,-0.535523,0,
-0.009786,-0.990206,0.13927,0,
0.978306,0.158405,-0.133513,0,
0.96707,0.236808,-0.093265,0,
-0.311001,0.469449,0.826375,0,
-0.813383,-0.418208,-0.404365,0,
0.781311,0.526031,-0.335923,0,
0.961069,0.108044,-0.254309,0,
-0.504965,-0.765577,-0.398624,0,
0.080302,0.884109,-0.46033,0,
-0.451685,0.889848,0.064429,0,
-0.98713,0.092179,-0.13068,0,
-0.980201,0.026119,-0.196275,0,
-0.735062,0.662884,-0.142367,0,
-0.904662,-0.423636,-0.046037,0,
0.341235,-0.660571,0.668734,0,
-0.016746,0.856105,-0.516531,0,
0.392371,-0.834366,-0.387142,0,
0.927426,-0.167551,0.334377,0,
0.787716,-0.604104,0.120671,0,
-0.127421,-0.655441,0.74442,0,
-0.509221,-0.155844,0.846408,0,
-0.205932,-0.487488,0.848497,0,
0.112332,-0.340894,-0.933366,0,
-0.762247,0.48178,0.432282,0,
0.075225,0.718021,0.691944,0,
-0.819874,0.564791,0.093905,0,
0.496734,-0.489457,0.71672,0,
-0.600362,-0.119114,0.790808,0,
0.929029,-0.148389,-0.338947,0,
0.643416,-0.536095,-0.546459,0,
0.118635,-0.836709,-0.534644,0,
0.461933,0.747449,-0.477428,0,
0.169024,-0.42074,-0.891296,0,
0.288774,0.528495,0.798313,0,
0.703132,-0.086662,-0.705759,0,
0.474584,-0.743589,-0.471005,0,
0.084313,-0.612482,0.785975,0,
-0.416436,-0.61563,-0.669014,0,
-0.18936,-0.668622,-0.719088,0,
0.763179,-0.535497,-0.361664,0,
0.689131,-0.298098,-0.660482,0,
0.402239,0.819925,0.407342,0,
-0.290787,0.850321,0.43863,0,
-0.116245,0.124645,0.985368,0,
0.92066,0.17792,-0.347463,0,
0.011703,-0.809531,0.58696,0,
-0.357123,-0.186219,-0.915306,0,
-0.333605,-0.931595,0.144358,0,
-0.60414,0.725095,-0.330533,0,
-0.73573,-0.085348,0.671876,0,
0.669414,0.62031,-0.408779,0,
-0.568966,-0.745348,-0.347467,0,
0.50216,-0.726708,0.468755,0,
0.065696,-0.743585,-0.665406,0,
0.527669,0.391357,0.753927,0,
0.679301,0.392984,-0.619769,0,
-0.300164,0.658028,0.69058,0,
0.598849,0.289445,0.746727,0,
-0.178902,0.983622,0.021934,0,
0.441316,0.726669,0.526491,0,
0.122133,-0.93606,-0.329962,0,
0.741988,0.62496,-0.242648,0,
0.549648,-0.603522,-0.577623,0,
0.268531,0.802747,-0.532436,0,
-0.68059,-0.605042,-0.413185,0,
-0.753218,-0.62548,-0.203563,0,
0.716535,0.607752,-0.342366,0,
0.210825,0.438669,0.873569,0,
0.612374,0.439314,-0.657268,0,
0.26704,0.005521,-0.96367,0,
-0.558574,-0.116505,-0.821232,0,
0.314196,0.790983,0.525002,0,
0.65875,0.364705,0.658057,0,
0.320691,0.153334,-0.93469,0,
0.417897,0.646039,0.638745,0,
0.48069,0.493299,-0.724979,0,
0.554578,0.562444,0.61327,0,
-0.543537,-0.813507,0.206816,0,
0.730243,-0.593015,0.339232,0,
-0.194012,0.875653,0.442257,0,
-0.466003,0.291951,-0.835228,0,
-0.302384,0.819067,-0.487538,0,
-0.177403,-0.706377,0.685245,0,
-0.842641,0.456491,-0.285608,0,
0.496712,-0.865211,0.068465,0,
0.178665,0.437405,-0.881337,0,
0.721768,-0.691414,0.031585,0,
0.595405,0.608183,0.524982,0,
-0.460218,-0.761702,0.456081,0,
0.048586,-0.992051,-0.116076,0,
-0.666964,-0.711486,0.221238,0,
0.994007,0.066756,0.086568,0,
-0.475778,-0.600766,0.64243,0,
0.682642,-0.472151,0.55774,0,
0.513332,0.556624,0.653193,0,
0.55373,0.391214,-0.735075,0,
-0.105898,-0.531675,0.840302,0,
-0.6056,-0.36381,0.707737,0,
-0.339207,-0.48191,0.807899,0,
-0.800867,0.597007,-0.046851,0,
-0.203862,0.714819,-0.668935,0,
-0.139197,-0.706138,0.694257,0,
-0.11238,0.840795,0.529561,0,
0.525643,-0.848304,0.063866,0,
0.694834,0.577811,0.428182,0,
-0.373286,0.867505,-0.328774,0,
-0.457187,0.567908,-0.684442,0,
-0.530759,0.017453,0.847343,0,
0.588164,-0.029516,0.808203,0,
-0.432747,-0.27238,0.859383,0,
0.58286,0.811803,0.035361,0,
0.684652,-0.252099,0.683884,0,
-0.078608,-0.742237,-0.665511,0,
0.869651,0.271795,0.41211,0,
0.764762,0.62952,-0.137274,0,
0.520727,0.187893,-0.83279,0,
-0.11442,-0.540829,0.833314,0,
-0.741722,0.207183,0.637906,0,
-0.359334,0.928015,0.098322,0,
0.713135,0.18654,0.675752,0,
-0.617203,-0.239321,-0.749523,0,
0.45031,-0.699809,-0.554517,0,
-0.727472,0.686064,0.010012,0,
-0.034218,-0.380479,0.924156,0,
0.845934,-0.044819,-0.531401,0,
0.722705,0.169262,0.67011,0,
-0.546493,0.041442,0.836438,0,
0.432794,-0.69354,0.575926,0,
-0.762004,0.199854,-0.615961,0,
-0.741828,0.301707,-0.598886,0,
0.439902,0.887086,0.139876,0,
-0.453379,-0.077634,0.887931,0,
-0.37651,-0.068101,0.923906,0,
-0.030251,-0.739007,-0.673018,0,
-0.350553,0.848883,-0.395613,0,
-0.7833,0.151923,0.602794,0,
-0.168618,0.234296,0.957431,0,
0.65187,-0.71287,-0.258617,0,
-0.964605,0.090798,-0.247573,0,
0.651133,-0.750595,0.112394,0,
0.192166,-0.938453,0.287016,0,
-0.899998,-0.397872,-0.17805,0,
0.175901,0.866606,-0.466961,0,
-0.702264,0.355326,0.616903,0,
0.630699,0.597749,0.494889,0,
0.79664,-0.552792,0.244511,0,
-0.043235,0.815831,-0.576672,0,
0.677584,-0.450309,0.581465,0,
-0.500867,0.451607,-0.738365,0,
0.577763,0.812059,-0.082159,0,
-0.011845,0.404007,0.914679,0,
-0.56779,0.106637,-0.816237,0,
-0.897373,0.382259,0.220455,0,
-0.632316,-0.304938,0.712173,0,
-0.719254,0.470799,-0.510903,0,
0.582597,-0.795359,-0.167288,0,
0.255895,-0.269633,0.92834,0,
0.308289,-0.919509,-0.243848,0,
-0.911147,-0.311754,-0.269484,0,
-0.728031,0.684114,-0.044263,0,
-0.696509,0.712062,-0.088565,0,
-0.090056,-0.548945,0.830993,0,
0.448406,-0.009888,0.893775,0,
-0.287555,-0.137969,-0.947775,0,
-0.303672,0.813595,0.495829,0,
0.584954,0.566219,0.58071,0,
-0.436795,-0.586633,-0.681961,0,
-0.699021,0.442486,0.561761,0,
-0.34345,0.916047,-0.207122,0,
0.70871,-0.694094,0.126347,0,
-0.908114,-0.237591,-0.344789,0,
0.11276,-0.157051,-0.981132,0,
-0.061565,0.331879,0.941311,0,
0.727988,0.160086,0.666638,0,
-0.600482,-0.267342,0.753625,0,
-0.646488,0.220329,-0.730417,0,
0.675094,-0.234307,-0.699534,0,
0.233207,0.664372,0.710088,0,
0.093598,0.35192,0.931338,0,
-0.020898,0.991041,0.131915,0,
-0.526084,-0.733819,0.42982,0,
0.828727,0.351252,0.435698,0,
-0.765217,-0.505728,0.398349,0,
-0.502016,0.846869,0.17548,0,
0.896665,-0.191316,0.399237,0,
-0.444235,-0.619848,0.646872,0,
-0.650219,-0.572638,0.4993,0,
0.763265,-0.390133,-0.514998,0,
0.193412,0.764259,-0.615223,0,
0.455637,0.643851,-0.614696,0,
-0.41624,-0.818404,0.396181,0,
-0.624399,-0.630897,0.460538,0,
-0.789139,-0.592449,-0.162062,0,
-0.493392,0.393836,0.775537,0,
-0.127291,-0.985975,0.107936,0,
0.383029,-0.855565,-0.348278,0,
0.917258,0.056264,0.3943,0,
0.220811,0.975293,0.006861,0,
0.648569,-0.383465,0.657505,0,
-0.715507,-0.131702,0.686079,0,
-0.504775,0.51498,-0.692819,0,
0.691206,0.660103,0.294104,0,
0.215175,-0.487384,-0.84626,0,
-0.315566,0.915418,0.249857,0,
0.054091,0.993211,0.10299,0,
0.325768,-0.889808,0.319558,0,
0.921005,0.060573,0.384814,0,
-0.534375,0.548281,-0.643298,0,
0.833761,0.405366,0.374862,0,
0.249603,-0.316023,-0.915329,0,
0.827629,-0.021273,-0.560872,0,
0.949652,-0.120942,-0.289024,0,
-0.773998,0.478167,0.41507,0,
-0.625875,0.607569,0.48902,0,
0.726667,-0.345808,-0.593609,0,
0.765424,0.290824,-0.574063,0,
0.58282,0.756641,0.296336,0,
0.797283,0.124404,-0.590646,0,
0.529387,0.732998,0.427158,0,
-0.32572,-0.847116,0.419881,0,
-0.024044,0.9645,-0.262985,0,
-0.024114,-0.76896,-0.638842,0,
0.242987,0.819909,0.51837,0,
0.548086,-0.386283,-0.741881,0,
0.012049,0.95343,-0.301374,0,
-0.492455,0.771375,-0.403073,0,
0.41355,-0.910119,-0.025673,0,
0.55888,0.813761,0.159521,0,
-0.34259,0.393432,-0.853138,0,
-0.80523,0.513202,0.297032,0,
-0.556261,-0.390823,-0.733369,0,
0.717896,-0.212355,0.662971,0,
-0.797378,0.510976,0.321079,0,
-0.554894,-0.605649,-0.570335,0,
-0.459536,-0.88806,-0.013228,0,
-0.6604,-0.637938,-0.396115,0,
-0.565434,0.688297,0.454458,0
};

		public static double GradientCoherentNoise(double x, double y, int seed,
			NoiseQuality noiseQuality){
			
			int x0 = (x > 0.0 ? (int)x : (int)x - 1);
			int x1 = x0 + 1;
			int y0 = (y > 0.0 ? (int)y : (int)y - 1);
			int y1 = y0 + 1;

			double xs = 0, ys = 0;
			switch (noiseQuality)
			{
				case NoiseQuality.Low:
					xs = (x - x0);
					ys = (y - y0);
					break;
				case NoiseQuality.Standard:
					xs = Loonim.Math.SCurve3(x - x0);
					ys = Loonim.Math.SCurve3(y - y0);
					break;
				case NoiseQuality.High:
					xs = Loonim.Math.SCurve5(x - x0);
					ys = Loonim.Math.SCurve5(y - y0);
					break;
			}

			double n0, n1, ix0, ix1;
			n0 = Gradient(x, y, x0, y0, seed);
			n1 = Gradient(x, y, x1, y0, seed);
			ix0 = Loonim.Math.LinearInterpolate(n0, n1, xs);
			n0 = Gradient(x, y, x0, y1, seed);
			n1 = Gradient(x, y, x1, y1, seed);
			ix1 = Loonim.Math.LinearInterpolate(n0, n1, xs);
			return Loonim.Math.LinearInterpolate(ix0, ix1, ys);
		}
		
		public static double GradientCoherentNoise(double x, double y, double z, int seed,
			NoiseQuality noiseQuality){
			
			int x0 = (x > 0.0 ? (int)x : (int)x - 1);
			int x1 = x0 + 1;
			int y0 = (y > 0.0 ? (int)y : (int)y - 1);
			int y1 = y0 + 1;
			int z0 = (z > 0.0 ? (int)z : (int)z - 1);
			int z1 = z0 + 1;

			double xs = 0, ys = 0, zs = 0;
			
			switch (noiseQuality)
			{
				case NoiseQuality.Low:
					xs = (x - x0);
					ys = (y - y0);
					zs = (z - z0);
					break;
				case NoiseQuality.Standard:
					xs = Loonim.Math.SCurve3(x - x0);
					ys = Loonim.Math.SCurve3(y - y0);
					zs = Loonim.Math.SCurve3(z - z0);
					break;
				case NoiseQuality.High:
					xs = Loonim.Math.SCurve5(x - x0);
					ys = Loonim.Math.SCurve5(y - y0);
					zs = Loonim.Math.SCurve5(z - z0);
					break;
			}
			
			// Now calculate the noise values at each vertex of the cube.  To generate
			// the coherent-noise value at the input point, interpolate these eight
			// noise values using the S-curve value as the interpolant (trilinear
			// interpolation.)
			double n0, n1, ix0, ix1, iy0, iy1;
			n0   = Gradient (x, y, z, x0, y0, z0, seed);
			n1   = Gradient (x, y, z, x1, y0, z0, seed);
			ix0  = Loonim.Math.LinearInterpolate (n0, n1, xs);
			n0   = Gradient (x, y, z, x0, y1, z0, seed);
			n1   = Gradient (x, y, z, x1, y1, z0, seed);
			ix1  = Loonim.Math.LinearInterpolate (n0, n1, xs);
			iy0  = Loonim.Math.LinearInterpolate (ix0, ix1, ys);
			n0   = Gradient (x, y, z, x0, y0, z1, seed);
			n1   = Gradient (x, y, z, x1, y0, z1, seed);
			ix0  = Loonim.Math.LinearInterpolate (n0, n1, xs);
			n0   = Gradient(x, y, z, x0, y1, z1, seed);
			n1   = Gradient (x, y, z, x1, y1, z1, seed);
			ix1  = Loonim.Math.LinearInterpolate (n0, n1, xs);
			iy1  = Loonim.Math.LinearInterpolate (ix0, ix1, ys);
			
			return Loonim.Math.LinearInterpolate (iy0, iy1, zs);
		}
		
		public static double GradientCoherentNoiseWrap(double x, double y, int wrap, int seed,
			NoiseQuality noiseQuality){
			
			int x0 = (x > 0.0 ? (int)x : (int)x - 1);
			int y0 = (y > 0.0 ? (int)y : (int)y - 1);
			
			double xs = 0, ys = 0;
			switch (noiseQuality)
			{
				case NoiseQuality.Low:
					xs = (x - x0);
					ys = (y - y0);
					break;
				case NoiseQuality.Standard:
					xs = Loonim.Math.SCurve3(x - x0);
					ys = Loonim.Math.SCurve3(y - y0);
					break;
				case NoiseQuality.High:
					xs = Loonim.Math.SCurve5(x - x0);
					ys = Loonim.Math.SCurve5(y - y0);
					break;
			}
			
			int x1 = x0 + 1;
			int y1 = y0 + 1;
			
			double n0, n1, ix0, ix1;
			n0 = GradientNoiseWrap(x, y,wrap, x0, y0, seed);
			n1 = GradientNoiseWrap(x, y,wrap, x1, y0, seed);
			ix0 = Loonim.Math.LinearInterpolate(n0, n1, xs);
			n0 = GradientNoiseWrap(x, y,wrap, x0, y1, seed);
			n1 = GradientNoiseWrap(x, y,wrap, x1, y1, seed);
			ix1 = Loonim.Math.LinearInterpolate(n0, n1, xs);
			return Loonim.Math.LinearInterpolate(ix0, ix1, ys);
		}
		
		private static double GradientNoiseWrap(double fx, double fy, int wrap,int ix,
			int iy, long seed){
			
			long vectorIndex = (
				  XNoiseGen * (ix % wrap)
				+ YNoiseGen * (iy % wrap)
				+ SeedNoiseGen * seed)
				& 0xffffffff;
			
			vectorIndex ^= (vectorIndex >> ShiftNoiseGen);
			vectorIndex = (vectorIndex & 0xff) << 2;
			double xvGradient = RandomVectors[vectorIndex];
			double yvGradient = RandomVectors[vectorIndex + 1];

			double xvPoint = (fx - ix);
			double yvPoint = (fy - iy);

			return ((xvGradient * xvPoint)
				+ (yvGradient * yvPoint)) * 2.12;
		}
		
		private static double Gradient(double fx, double fy, int ix,
			int iy, long seed){
			
			long vectorIndex = (
				  XNoiseGen * ix
				+ YNoiseGen * iy
				+ SeedNoiseGen * seed)
				& 0xffffffff;
			
			vectorIndex ^= (vectorIndex >> ShiftNoiseGen);
			vectorIndex = (vectorIndex & 0xff) << 2;
			double xvGradient = RandomVectors[vectorIndex];
			double yvGradient = RandomVectors[vectorIndex + 1];

			double xvPoint = (fx - ix);
			double yvPoint = (fy - iy);

			return ((xvGradient * xvPoint)
				+ (yvGradient * yvPoint)) * 2.12;
		}
		
		private static double Gradient(double fx, double fy, double fz, int ix,
			int iy, int iz, long seed){
			
			long vectorIndex = (
				  XNoiseGen * ix
				+ YNoiseGen * iy
				+ ZNoiseGen * iz
				+ SeedNoiseGen * seed)
				& 0xffffffff;
			
			vectorIndex ^= (vectorIndex >> ShiftNoiseGen);
			vectorIndex = (vectorIndex & 0xff) << 2;
			double xvGradient = RandomVectors[vectorIndex];
			double yvGradient = RandomVectors[vectorIndex + 1];
			double zvGradient = RandomVectors[vectorIndex + 2];

			double xvPoint = (fx - ix);
			double yvPoint = (fy - iy);
			double zvPoint = (fz - iz);

			return ((xvGradient * xvPoint)
				+ (yvGradient * yvPoint)
				+ (zvGradient * zvPoint)
				) * 2.12;
		}
		
	}
	
}