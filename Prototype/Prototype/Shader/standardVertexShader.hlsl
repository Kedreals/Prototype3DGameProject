struct VOut
{
	float4 position : SV_POSITION;
	float4 color    : COLOR;
};

cbuffer Ratio : register(b0){
	float4x4 ratio;
}

cbuffer InvertCamera : register(b1) {
	float4x4 camara;
}

VOut main(float4 position : POSITION, float4 color : COLOR)
{
	VOut output;

	//scaling according to the z achse
	float4 p = mul(camara, position);
	p = float4(p[0] / p[2], p[1] / p[2], p[2] - abs(p[2]), 1);


	//setting the values for the pixelshader
	//output.position = p;
	output.position = mul(ratio,p);
	output.color = color;

	return output;
}